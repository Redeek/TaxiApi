using TaxiApi.Entities;

namespace TaxiApi.Models
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ContractNumber { get; set; }
        public DateTime StartOfContractNumber { get; set; }
        public DateTime EndOfContractNumber { get; set; }

    }
}
