using Models.Request;
using Models.Response;
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
        Task<string> CreateBookingDetails(BookingRequest bookingRequest, int? bookingId);
        Task<string> AddFoodDetails(List<FoodTransactionRequest> foodTransactionRequests, int customerId);
        Task<BillingResponse> GetBillingDetails(int customerId);
    }
}