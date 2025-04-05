using System.ComponentModel.DataAnnotations;
using TaxiApi.Entities;

namespace TaxiApi.Models
{
    public class CreateCarDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Plate { get; set; }

        public string Category { get; set; }

        public bool Damage { get; set; }

    }
}
