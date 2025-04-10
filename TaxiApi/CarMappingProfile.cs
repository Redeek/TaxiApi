using AutoMapper;
using TaxiApi.Entities;
using TaxiApi.Models;

namespace TaxiApi
{
    public class CarMappingProfile: Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>();

            CreateMap<Driver, DriverDto>();

            CreateMap<CreateCarDto, Car>();

            CreateMap<CreateDriverDto, Driver>();
        }
    }
}
