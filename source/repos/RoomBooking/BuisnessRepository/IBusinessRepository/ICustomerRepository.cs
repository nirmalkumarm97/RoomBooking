﻿using Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessRepository.IBusinessRepository
{
    public interface ICustomerRepository
    {
        Task<string> CreateCustomer(CustomerRequest customerRequest);
        Task<string> CreateBookingDetails(BookingRequest bookingRequest, int? bookingId);
        Task<string> AddFoodDetails (FoodTransactionRequest foodTransactionRequest , int customerId);
    }
}
