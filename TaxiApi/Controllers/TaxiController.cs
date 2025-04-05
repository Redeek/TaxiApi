using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiApi.Entities;
using TaxiApi.Models;

namespace TaxiApi.Controllers
{
    [Route("api/taxi")]
    public class TaxiController : ControllerBase
    {
        private readonly TaxiDbContext _dbContext;
        private readonly IMapper _mapper;

        public TaxiController(TaxiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var cars = _dbContext.Cars
                .Include(c => c.Drivers)
                    .ThenInclude(d=> d.Cars)
                .ToList();

            var carsDtos = _mapper.Map<List<CarDto>>(cars);

            return Ok(carsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDto> GetCar([FromRoute] int id)
        {
            var car = _dbContext
                .Cars
                .Include(d=> d.Drivers)
                .FirstOrDefault(c => c.Id == id);

            if (car is null)
            {
                NotFound();
            }

            var carDto = _mapper.Map<CarDto>(car);
            return Ok(carDto);
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = _mapper.Map<Car>(dto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return Created($"/api/taxi/{car.Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar([FromRoute] int id)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(c => c.Id == id);

            if (car is null)
            {
                return BadRequest("Car not found");
            }

            _dbContext
                .Cars
                .Remove(car);
            _dbContext.SaveChanges();

            return Ok($"Deleted car {id}");

        }

    }
}
