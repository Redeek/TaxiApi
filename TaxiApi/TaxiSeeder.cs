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
                            Name = "Mateusz",
                            Surname = "Rdestowicz",
                            ContractNumber = "34e813tsa942",
                            IdNumber = "12345678901",
                            StartOfContractNumber = new DateTime(2025,1,13),
                            EndOfContractNumber = new DateTime(2026,1,30)
                        },
                        new Driver()
                        {
                            Name = "Jarek",
                            Surname = "Zabłocki",
                            ContractNumber = "342111dsa813tsa942",
                            IdNumber = "12343278901",
                            StartOfContractNumber = new DateTime(2025,1,13),
                            EndOfContractNumber = new DateTime(2026,1,30)
                        }
                    }
                }
            };
        }
    }
}
