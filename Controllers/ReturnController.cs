using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Contracts;

namespace RentalCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnController : ControllerBase
    {
        private readonly IReturnService _returnService;

        public ReturnController(IReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _returnService.GetAll();
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
