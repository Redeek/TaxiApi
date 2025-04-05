using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaxiApi.Entities;
using TaxiApi.Models;

namespace TaxiApi.Services
{
    public interface ICarService
    {
        CarDto GetById(int id);
        IEnumerable<CarDto> GetAll();
        int Create(CreateCarDto dto);
        bool Delete(int id);
    }

    public class CarService : ICarService
    {
        private readonly TaxiDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarService(TaxiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public CarDto GetById(int id)
        {
            var car = _dbContext
                .Cars
                .Include(d => d.Drivers)
                .FirstOrDefault(c => c.Id == id);

            if (car is null)
            {
                return null;
            }

            var result = _mapper.Map<CarDto>(car);

            return result;
        }


        public IEnumerable<CarDto> GetAll()
        {
            var cars = _dbContext.Cars
                .Include(c => c.Drivers)
                .ThenInclude(d => d.Cars)
                .ToList();

            var carsDtos = _mapper.Map<List<CarDto>>(cars);

            return carsDtos;
        }

        public int Create(CreateCarDto dto)
        {

            var car = _mapper.Map<Car>(dto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return car.Id;
        }

        public bool Delete(int id)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(c => c.Id == id);

            if (car is null)
            {
                return false;
            }

            _dbContext
                .Cars
                .Remove(car);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
