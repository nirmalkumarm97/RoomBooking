using BuisnessLogics.IBusinessLogics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogics _userLogics;
        public UserController(IUserLogics userLogics)
        {
            _userLogics = userLogics;
        }
        [HttpPost("CreateUsers")]
        public async Task<IActionResult> CreateUsers(List<UserRequest> userRequest)
        {
            try
            {
                return Ok(await _userLogics.CreateUsers(userRequest) ?? throw new InvalidOperationException());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUsers")]
        public async Task<IActionResult> DeleteUsers(List<int> Ids)
        {
            try
            {
                return Ok(await _userLogics.DeleteUsers(Ids) ?? throw new InvalidOperationException());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
       public async Task<IActionResult> UpdateUser(UserRequest userRequest, int Id)
        {
            try
            {
                return Ok(await _userLogics.UpdateUser(userRequest, Id) ?? throw new InvalidOperationException());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateRequest authenticate)
        {
            try
            {
                return Ok(await _userLogics.AuthenticateUser(authenticate) ?? throw new InvalidOperationException());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

