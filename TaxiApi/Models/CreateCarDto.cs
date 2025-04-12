using System.ComponentModel.DataAnnotations;
using TaxiApi.Entities;

namespace TaxiApi.Models
{
    public class CreateCarDto
    {
        public string Name { get; set; }
        public string Plate { get; set; }

        public string Category { get; set; }

        public bool Damage { get; set; }

    }
}
