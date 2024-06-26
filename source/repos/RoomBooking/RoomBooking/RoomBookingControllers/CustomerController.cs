﻿using BuisnessLogics.IBusinessLogics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;

namespace Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerLogics _customerLogics;
        public CustomerController(ICustomerLogics customerLogics)
        {
            _customerLogics = customerLogics;
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerRequest customerRequest)
        {
            try
            {
                return Ok(await _customerLogics.CreateCustomer(customerRequest) ?? throw new InvalidOperationException());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateBookingDetails")]
        public async Task<IActionResult> CreateBookingDetails(BookingRequest bookingRequest, int? bookingId)
        {
            try
            {
                return Ok(await _customerLogics.CreateBookingDetails(bookingRequest, bookingId) ?? throw new InvalidOperationException());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostFoodTransaction(List<FoodTransactionRequest> foodTransactionRequests, int customerId)
        {
            if (foodTransactionRequests == null)
            {
                return BadRequest("Invalid request.");
            }
            string result = await _customerLogics.AddFoodDetails(foodTransactionRequests, customerId) ?? throw new InvalidOperationException();

            return Ok(new { Message = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetBillingDetails(int customerId)
        {
            if (customerId <= 0)
            {
                return BadRequest("Invalid request.");
            }
            try
            {
                var response = await _customerLogics.GetBillingDetails(customerId);
                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

