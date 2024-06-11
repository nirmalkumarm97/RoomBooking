using BuisnessRepository.IBusinessRepository;
using Data.NewFolder;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Models;
using Models.Request;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessRepository.BusinessRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RoomBookingDbContext _roomBookingDbContext;
        private static int lastId = 1000; // starting ID
        public CustomerRepository(RoomBookingDbContext roomBookingDbContext)
        {
            _roomBookingDbContext = roomBookingDbContext;
        }
        public async Task<string> CreateCustomer(CustomerRequest customerRequest)
        {
            // Input validation
            if (customerRequest == null)
            {
                throw new ArgumentNullException(nameof(customerRequest), "Customer request cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(customerRequest.CustomerName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(customerRequest.CustomerName));
            }
            if (string.IsNullOrWhiteSpace(customerRequest.Address))
            {
                throw new ArgumentException("Address cannot be empty.", nameof(customerRequest.Address));
            }
            if (customerRequest.DateOfBirth == default)
            {
                throw new ArgumentException("Date of birth must be specified.", nameof(customerRequest.DateOfBirth));
            }
            if (string.IsNullOrWhiteSpace(customerRequest.Proof))
            {
                throw new ArgumentException("Proof must be specified.", nameof(customerRequest.Proof));
            }
            string filepath = $"C:\\RoomBookingFile\\{customerRequest.CustomerName}";
            var customerDetails = new CustomerDetails()
            {
                CustomerName = customerRequest.CustomerName,
                DateOfBirth = DateOnly.Parse(customerRequest.DateOfBirth),
                Proof = SaveBase64AsPdf(customerRequest.Proof, filepath),
                Address = customerRequest.Address,
                CreatedAt = DateTime.UtcNow,
            };

            try
            {
                // Using a transaction to ensure all operations are atomic
                using (var transaction = await _roomBookingDbContext.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    await _roomBookingDbContext.CustomerDetails.AddAsync(customerDetails).ConfigureAwait(false);
                    await _roomBookingDbContext.SaveChangesAsync().ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Log database-related exceptions
                // Assuming _logger is an instance of a logger, e.g., ILogger<CreateCustomerService>
                // _logger.LogError(dbEx, "Database update exception while saving customer details.");
                throw new ApplicationException("An error occurred while saving customer details to the database. Please try again later.", dbEx);
            }
            catch (Exception ex)
            {
                // Log other exceptions
                // _logger.LogError(ex, "An unexpected error occurred while saving customer details.");
                throw new ApplicationException("An unexpected error occurred while saving customer details. Please try again later.", ex);
            }

            return $"{customerDetails.CustomerName} details saved successfully.";
        }
        private string SaveBase64AsPdf(string base64String, string filePath)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentNullException(nameof(base64String), "Base64 string is null or empty.");
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            else
            {
                File.Delete(filePath);
            }

            byte[] pdfBytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, pdfBytes);
            return filePath;
        }
        public async Task<string> CreateBookingDetails(BookingRequest bookingRequest, string? bookingId)
        {
            if (bookingRequest == null)
                throw new ArgumentNullException(nameof(bookingRequest));

            if (bookingRequest.CustomerId <= 0)
                throw new ArgumentException("Invalid Customer ID.");

            if (bookingRequest.FromDateTime == default)
                throw new ArgumentException("Invalid From DateTime.");

            if (bookingRequest.AdultCount <= 0)
                throw new ArgumentException("Adult count must be greater than 0.");

            if (bookingRequest.CreatedBy <= 0)
                throw new ArgumentException("Invalid Created By ID.");

            if (string.IsNullOrWhiteSpace(bookingId))
            {
                var lastBooking = _roomBookingDbContext.BookingDetails.Where(x => x.BookingId == bookingId).FirstOrDefault();
                if (lastBooking != null)
                {
                    lastBooking.FromDateTime = bookingRequest.FromDateTime;
                    lastBooking.ToDateTime = bookingRequest.ToDateTime;
                    lastBooking.CustomerId = bookingRequest.CustomerId;
                    lastBooking.RoomType = bookingRequest.RoomType;
                    lastBooking.AdultCount = bookingRequest.AdultCount;
                    lastBooking.ChildrenCount = bookingRequest.ChildrenCount;
                    lastBooking.ModifiedBy = bookingRequest.CreatedBy; // Assuming `UpdatedBy` is tracked
                    lastBooking.ModifiedAt = DateTime.UtcNow;
                }

                // Save the updated booking to the database
                await _roomBookingDbContext.SaveChangesAsync();
                return "Update succeeded";

            }
            else
            {
                var booking = new BookingDetails
                {
                    BookingId = GenerateBookingId(bookingId),
                    FromDateTime = bookingRequest.FromDateTime,
                    ToDateTime = bookingRequest.ToDateTime,
                    CustomerId = bookingRequest.CustomerId,
                    RoomType = bookingRequest.RoomType,
                    AdultCount = bookingRequest.AdultCount,
                    ChildrenCount = bookingRequest.ChildrenCount,
                    CreatedBy = bookingRequest.CreatedBy,
                    CreatedAt = DateTime.UtcNow
                };

                // Save to the database
                _roomBookingDbContext.BookingDetails.Add(booking);
                await _roomBookingDbContext.SaveChangesAsync();
                return "Create Succeed";
            }
        }
        private string GenerateBookingId(string? bookingId)
        {
            var lastBooking = _roomBookingDbContext.BookingDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastBooking == null)
            {
                return "B" + lastId; // Return the new ID prefixed with 'B'
            }
            else if (bookingId == null)
            {
                string newbookingId = bookingId.Replace("B", "");
                int number = Convert.ToInt32(newbookingId);
                number++;
                string result = "B" + number.ToString();
                return result;
            }
            else return bookingId;
        }
    }
}
