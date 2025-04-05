﻿using TaxiApi.Entities;

namespace TaxiApi.Models
{
    public class CarDto
    {
        public string Name { get; set; }
        public string Plate { get; set; }

        public string Category { get; set; }

        public List<DriverDto> Drivers { get; set; }
    }
}
