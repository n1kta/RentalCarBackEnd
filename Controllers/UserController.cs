using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Contracts;
using RentalCar.Dtos;

namespace RentalCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            try
            {
                var result = await _userService.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync(TokenDto model)
        {
            try
            {
                var result = await _userService.GetTokenAsync(model);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("update/{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody]CustomerUpdateDto dto)
        {
            try
            {
                await _userService.UpdateCustomer(customerId, dto);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("getCurrent")]
        public async Task<IActionResult> GetCurrentCustomer()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                var result = await _userService.GetCurrentCustomer(token);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("isAdmin")]
        public async Task<IActionResult> IsAdmin()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                var result = await _userService.IsAdmin(token);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
