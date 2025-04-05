using AutoMapper;
using TaxiApi.Entities;
using TaxiApi.Models;

namespace TaxiApi
{
    public class CarMappingProfile: Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(m => m.Drivers, c => c.MapFrom(s => s.Drivers));

            CreateMap<Driver, DriverDto>();

            CreateMap<CreateCarDto, Car>();
        }
    }
}
