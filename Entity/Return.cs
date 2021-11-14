using System;
using RentalCar.Models;

namespace RentalCar.Entity
{
    public class Return : BaseEntity
    {
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public DateTime ReturnDate { get; set; } = DateTime.Now;
        public bool InTime { get; set; }
    }
}
