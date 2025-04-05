using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiApi.Entities;
using TaxiApi.Models;
using TaxiApi.Services;

namespace TaxiApi.Controllers
{
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService _carService)
        {
            this._carService = _carService;
        }


        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var carsDtos = _carService.GetAll();

            return Ok(carsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDto> GetCar([FromRoute] int id)
        {

            var car = _carService.GetById(id);

            if (car is null)
            {
                NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = _carService.Create(dto);

            return Created($"/api/taxi/{car}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar([FromRoute] int id)
        {

            var car = _carService.Delete(id);

            if (car is false)
            {
                return NotFound("Car not found");
            }

            return Ok($"Deleted car {id}");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCar([FromBody] EditCarDto dto, [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var car = _carService.Edit(dto, id);

            if (!car)
            {
                return NotFound();
            }

            return Ok($"Car {id} is updated");

        }

    }
}
