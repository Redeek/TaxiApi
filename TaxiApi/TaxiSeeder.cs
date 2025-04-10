using TaxiApi.Entities;

namespace TaxiApi
{
    public class TaxiSeeder
    {
        private readonly TaxiDbContext _dbContext;

        public TaxiSeeder(TaxiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Cars.Any())
                {
                    var cars = GetCars();
                    _dbContext.Cars.AddRange(cars);
                    _dbContext.SaveChanges();
                }
            }
        }


        private IEnumerable<Car> GetCars()
        {
            return new List<Car>()
            {
                new Car()
                {
                    Name = "Corolla",
                    Plate = "LU4196F",
                    Category = "Uber",
                    Damage = false,
                    Drivers = new List<Driver>()
                    {
                        new Driver()
                        {
                            ContractNumber = "34e813tsa942",
                            IdNumber = "12345678901",
                            StartOfContractNumber = new DateTime(2025,1,13),
                            EndOfContractNumber = new DateTime(2026,1,30),
                            User = new User()
                            {
                                Email = "mati.red@mail.com",
                                Name = "Mateusz",
                                Surname = "Rdest",
                                PhoneNumber = "123123123"
                            }
                        },
                      
                    }
                }
            };
        }

        private IEnumerable<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "Driver"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
        }
    }
}
