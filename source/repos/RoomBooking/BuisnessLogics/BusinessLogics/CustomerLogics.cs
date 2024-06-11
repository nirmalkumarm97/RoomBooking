using BuisnessLogics.IBusinessLogics;
using BuisnessRepository.IBusinessRepository;
using Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogics.BusinessLogics
{
    public class CustomerLogics : ICustomerLogics
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerLogics(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<string> CreateCustomer(CustomerRequest customerRequest) => _customerRepository.CreateCustomer(customerRequest);
        public Task<string> CreateBookingDetails(BookingRequest bookingRequest, int? bookingId) => _customerRepository.CreateBookingDetails(bookingRequest, bookingId);
        public Task<string> AddFoodDetails(List<FoodTransactionRequest> foodTransactionRequests , int customerId) => _customerRepository.AddFoodDetails(foodTransactionRequests , customerId);
    }
}
