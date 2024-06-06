using BuisnessRepository.IBusinessRepository;
using Data.NewFolder;
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

            var customerDetails = new CustomerDetails()
            {
                CustomerName = customerRequest.CustomerName,
                DateOfBirth = customerRequest.DateOfBirth,
                Proof = customerRequest.Proof,
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
    }
}
