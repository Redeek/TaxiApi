using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaxiApi.Entities;
using TaxiApi.Exceptions;
using TaxiApi.Models;

namespace TaxiApi.Services
{
    public interface ICarService
    {
        CarDto GetById(int id);
        IEnumerable<CarDto> GetAll();
        int Create(CreateCarDto dto);
        void Delete(int id);

        void Edit(EditCarDto dto, int id);
    }

    public class CarService : ICarService
    {
        private readonly TaxiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CarService> _logger;

        public CarService(TaxiDbContext dbContext, IMapper mapper, ILogger<CarService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public CarDto GetById(int id)
        {
            var car = _dbContext
                .Cars
                .Include(d => d.Drivers)
                .FirstOrDefault(c => c.Id == id);

            if (car is null)
                throw new NotFoundException("Car not found");

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

        public void Delete(int id)
        {
            _logger.LogError($"Car with id: {id} DELETE action invoke");
            var car = _dbContext
                .Cars
                .FirstOrDefault(c => c.Id == id);

            if (car is null)
                throw new NotFoundException("Car not found");

            _dbContext
                .Cars
                .Remove(car);
            _dbContext.SaveChanges();

        }

        public void Edit(EditCarDto dto, int id)
        {
            var editCar = _dbContext
                .Cars
                .FirstOrDefault(c => c.Id == id);

            if (editCar is null)
                throw new NotFoundException("Car not found");

            editCar.Name = dto.Name;
            editCar.Plate = dto.Plate;
            editCar.Category = dto.Category;

            _dbContext.SaveChanges();

            
        }
    }
}
