using TaxiApi.Entities;

namespace TaxiApi.Models
{
    public class CreateDriverDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string ContractNumber { get; set; }
        public DateTime StartOfContractNumber { get; set; }
        public DateTime EndOfContractNumber { get; set; }

    }
}
