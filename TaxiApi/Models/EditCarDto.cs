using System.ComponentModel.DataAnnotations;

namespace TaxiApi.Models
{
    public class EditCarDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MaxLength(7)]
        public string Plate { get; set; }
        [Required]
        [MaxLength(25)]
        public string Category { get; set; }
    }
}
