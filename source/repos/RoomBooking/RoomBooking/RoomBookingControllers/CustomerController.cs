using BuisnessLogics.IBusinessLogics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

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
        [HttpPost("CreateBookingDetails")]
        public async Task<IActionResult> CreateBookingDetails(BookingRequest bookingRequest, string? bookingId)
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
    }
}

