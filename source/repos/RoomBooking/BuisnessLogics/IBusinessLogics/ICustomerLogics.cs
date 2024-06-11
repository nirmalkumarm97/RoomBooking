using Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogics.IBusinessLogics
{
    public interface ICustomerLogics 
    {
        Task<string> CreateCustomer(CustomerRequest customerRequest);
        Task<string> CreateBookingDetails(BookingRequest bookingRequest, string? bookingId);
    }
}