using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Contracts;
using RentalCar.Dtos;
using RentalCar.Models;

namespace RentalCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _carService.GetById(id);
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
                var result = await _carService.GetAll();
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("getAvailable")]
        public async Task<IActionResult> GetAvailable([FromBody] FilterDto dto)
        {
            try
            {
                var result = await _carService.GetAvailable(dto);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CarDto dto)
        {
            try
            {
                await _carService.Create(dto);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CarDto dto)
        {
            try
            {
                await _carService.Update(id, dto);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _carService.Delete(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
