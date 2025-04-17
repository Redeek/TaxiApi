using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;
using TaxiApi.Entities;
using TaxiApi.Models;
using TaxiApi.Services;

namespace TaxiAPI.Tests
{
    public class DriverServiceTests
    {
        private readonly Mock<TaxiDbContext> _dbContextMock;
        private readonly Mock<IMapper> _mockMapper;

        private readonly DriverService _driverService;
        private readonly Mock<ILogger> _mockLogger;

        public DriverServiceTests()
        {
            _dbContextMock = new Mock<TaxiDbContext>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger>();

            _driverService = new DriverService(_dbContextMock.Object, _mockMapper.Object);
        }

        [Fact]
        public void AssignDriverToCar_ShouldAssignCarToDriver_WhenDataIsValid()
        {
            var driverId = 1;
            var carId = 2;

            var car = new Car { Id = carId };
            var driver = new Driver { Id = driverId, Cars = new List<Car>() };

            var cars = new List<Car> { car }.AsQueryable().BuildMockDbSet();
            var drivers = new List<Driver> { driver }.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(db => db.Cars).Returns(cars.Object);
            _dbContextMock.Setup(db => db.Drivers).Returns(drivers.Object);
            _dbContextMock.Setup(db => db.SaveChanges()).Verifiable();

            var service = new DriverService(_dbContextMock.Object, _mockMapper.Object);

            service.AssignDriverToCar(driverId, carId);

            // Assert
            Assert.Single(driver.Cars);
            Assert.Equal(carId, driver.Cars.First().Id);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }



    }

    public static class MockQueryableExtensions
    {
        public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
        {
            var mock = new Mock<DbSet<T>>();
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => source.GetEnumerator());
            return mock;
        }
    }
}
