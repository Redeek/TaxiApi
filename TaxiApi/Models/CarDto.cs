using TaxiApi.Entities;

namespace TaxiApi.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }

        public string Category { get; set; }
        public bool Damage { get; set; }

    }
}
