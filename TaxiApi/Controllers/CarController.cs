using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TaxiApi.Entities;
using TaxiApi.Models;
using TaxiApi.Services;

namespace TaxiApi.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService _carService)
        {
            this._carService = _carService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var carsDtos = _carService.GetAll();

            return Ok(carsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDto> GetCar([FromRoute] int id)
        {

            var car = _carService.GetById(id);

            return Ok(car);
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto dto)
        {

            var car = _carService.Create(dto);

            return Created($"/api/car/{car}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar([FromRoute] int id)
        {

            _carService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCar([FromBody] EditCarDto dto, [FromRoute]int id)
        {
          
            _carService.Edit(dto, id);


            return Ok($"Car {id} is updated");

        }

    }
}
