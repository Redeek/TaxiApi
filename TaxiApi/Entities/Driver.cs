using System.Runtime.InteropServices.JavaScript;

namespace TaxiApi.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string ContractNumber { get; set; }
        public DateTime StartOfContractNumber { get; set; }
        public DateTime EndOfContractNumber { get; set; }

        public virtual List<Car> Cars { get; set; }

    }
}
