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
        void DeleteDriver(int driverId);
        IEnumerable<DriverDataDto> GetAll();
        public void AssignDriverToCar(int driverId, int carId);
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


        public void AssignDriverToCar(int driverId, int carId)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
            if (car is null)
                throw new NotFoundException("Car not found");

            var driver = _context.Drivers.FirstOrDefault(d => d.Id == driverId);
            if (driver is null)
                throw new NotFoundException("User not found");

            driver.Cars ??= new List<Car>();
            driver.Cars.Add(car);
            _context.SaveChanges();

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

        public IEnumerable<DriverDataDto> GetAll()
        {
            var drivers = _context.Drivers
                .Include(d => d.User)
                .ToList();

            var driverDto = _mapper.Map<List<DriverDataDto>>(drivers);

            return driverDto;
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

        public void DeleteDriver(int driverId)
        {
            var driver = _context
                .Drivers
                .FirstOrDefault(d => d.Id == driverId);

            if (driver is null)
                throw new NotFoundException("Driver not Found");

            _context.Drivers.Remove(driver);
            _context.SaveChanges();

        }

    }
}
