using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Contracts;

namespace RentalCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _customerService.GetById(id);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _customerService.GetAll();
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }
    }
}
