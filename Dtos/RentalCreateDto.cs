using System;

namespace RentalCar.Dtos
{
    public class RentalCreateDto
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string PickUpLoc { get; set; }
        public string DropOffLoc { get; set; }
        public decimal Fee { get; set; }
    }
}