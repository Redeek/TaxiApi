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

            CreateMap<Driver, DriverDataDto>()
                .ForMember(u => u.Name, o => o.MapFrom(s => s.User.Name))
                .ForMember(u => u.Email, o => o.MapFrom(s => s.User.Email))
                .ForMember(u => u.Surname, o => o.MapFrom(s => s.User.Surname))
                .ForMember(u => u.PhoneNumber, o => o.MapFrom(s => s.User.PhoneNumber));
        }
    }
}
