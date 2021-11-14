using System;
using RentalCar.Models;

namespace RentalCar.Entity
{
    public class Rental : BaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer{ get; set; }
        public Return Return { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string PickUpLoc { get; set; }
        public string DropOffLoc { get; set; }
        public decimal Fee { get; set; }
    }
}
