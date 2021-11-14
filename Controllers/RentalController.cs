using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Contracts;
using RentalCar.Dtos;

namespace RentalCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
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
                var result = await _rentalService.GetAll();
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]RentalCreateDto dto)
        {
            try
            {
                await _rentalService.Create(dto);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
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

        [HttpGet("getByCustomerId/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            try
            {
                var result = await _rentalService.GetByCustomerId(customerId);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("return/{rentalId}")]
        public async Task<IActionResult> ReturnRental(int rentalId)
        {
            try
            {
                await _rentalService.ReturnRental(rentalId);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
