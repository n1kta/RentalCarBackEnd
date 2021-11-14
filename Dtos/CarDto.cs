using System;
using RentalCar.Enums;

namespace RentalCar.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Available { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime Year { get; set; }
        public FuelType Fuel { get; set; }
        public string Photo { get; set; }
    }
}