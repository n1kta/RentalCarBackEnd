using System;

namespace RentalCar.Dtos
{
    public class RentalDto
    {
        public string Car { get; set; }
        public string Customer { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public decimal Fee { get; set; }
    }
}