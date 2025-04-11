using Microsoft.AspNetCore.Identity;
using TaxiApi.Entities;

namespace TaxiApi
{
    public class TaxiSeeder
    {
        private readonly TaxiDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public TaxiSeeder(TaxiDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
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

                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
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
            var user = new User()
            {
                Email = "driver.test@test.pl",
                Name = "Kamil",
                Surname = "Machnio",
                PhoneNumber = "123123123",
                RoleId = 1
            };

            user.Password = _passwordHasher.HashPassword(user, "Password1");

            var driver = new Driver()
            {
                ContractNumber = "E1231EEEAASD42",
                IdNumber = "12312312311",
                StartOfContractNumber = new DateTime(2025, 1, 13),
                EndOfContractNumber = new DateTime(2026, 12, 1),
                User = user
            };

            var car = new Car()
            {
                Name = "Corolla",
                Plate = "LU4196F",
                Category = "Uber",
                Damage = false,
                Drivers = new List<Driver> { driver }
            };


            return new List<Car> { car };

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

        private IEnumerable<User> GetUsers()
        {
            var manago = new User()
            {
                Email = "manager1@gmail.com",
                Name = "Manago",
                Surname = "Mango",
                PhoneNumber = "123123123",
                RoleId = 2
            };
            manago.Password = _passwordHasher.HashPassword(manago, "Password1");

            var admin = new User()
            {
                Email = "admin1@gmail.com",
                Name = "Admin",
                Surname = "Admin",
                PhoneNumber = "123123123",
                RoleId = 3
            };
            admin.Password = _passwordHasher.HashPassword(admin, "Password1");

            return new List<User> { manago, admin };

        }
    }
}
