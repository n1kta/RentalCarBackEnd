using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RentalCar.Enums;
using RentalCar.Models;

namespace RentalCar.Entity
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Available { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime Year { get; set; }
        public FuelType Fuel { get; set; }
        [StringLength(int.MaxValue)]
        public string ImageUrl { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
