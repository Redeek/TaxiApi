using Microsoft.AspNetCore.Mvc;
using TaxiApi.Models;
using TaxiApi.Services;

namespace TaxiApi.Controllers
{

    [Route("api/driver")]
    [ApiController]
    public class DriverController: ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost("/car/{carId}")]
        public ActionResult CreateDriver([FromRoute] int carId, [FromBody]CreateDriverDto dto)
        {
            var newDriver = _driverService.CreateDriverForCar(carId, dto);

            return Created($"api/car/{carId}/driver/{newDriver}", null);
        }

        [HttpGet("/car/{carId}")]
        public ActionResult<IEnumerable<DriverDto>> GetAllDriversByCarId([FromRoute] int carId)
        {
            var drivers = _driverService.GetAllDriversByCarId(carId);

            return Ok(drivers);
        }

        [HttpGet("{driverId}")]
        public ActionResult<DriverDto> GetDriverById([FromRoute] int driverId)
        {
            var driver = _driverService.GetDriverById(driverId);

            return Ok(driver);
        }
             
    }
}
