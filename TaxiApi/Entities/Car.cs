namespace TaxiApi.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }
         
        public string Category { get; set; }

        public bool Damage { get; set; }

        public virtual List<Driver> Drivers { get; set; }

    }
}
