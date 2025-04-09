using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaxiApi.Entities;
using TaxiApi.Exceptions;
using TaxiApi.Models;

namespace TaxiApi.Services
{
    public interface IDriverService
    {
        int CreateDriverForCar(int carId, CreateDriverDto dto);
        IEnumerable<DriverDto> GetAllDriversByCarId(int carId);
        DriverDto GetDriverById(int driverId);
    }

    public class DriverService : IDriverService
    {
        private readonly TaxiDbContext _context;
        private readonly IMapper _mapper;

        public DriverService(TaxiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public int CreateDriverForCar(int carId, CreateDriverDto dto)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);

            if (car is null)
                throw new NotFoundException("Car not found");

            var driver = _mapper.Map<Driver>(dto);

            driver.Cars = new List<Car> { car };

            _context.Drivers.Add(driver);
            _context.SaveChanges();

            return driver.Id;
        }


        public IEnumerable<DriverDto> GetAllDriversByCarId(int carId)
        {
            var car = _context
                .Cars
                .Include(c=> c.Drivers)
                .FirstOrDefault(c => c.Id == carId);

            if (car is null)
                throw new NotFoundException("Car not found");

            var result = _mapper.Map<List<DriverDto>>(car.Drivers);

            return result;
        }

        public DriverDto GetDriverById(int driverId)
        {
            var driver = _context
                .Drivers
                .FirstOrDefault(d => d.Id == driverId);

            if (driver is null)
                throw new NotFoundException("Driver not Found");

            var result = _mapper.Map<DriverDto>(driver);

            return result;
        }

    }
}
