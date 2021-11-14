using System;

namespace RentalCar.Dtos
{
    public class RentalByCustomerDto
    {
        public int RentalId { get; set; }
        public string Car { get; set; }
        public string Photo { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string PickUpLoc { get; set; }
        public string DropOffLoc { get; set; }
        public bool IsReturned { get; set; }
    }
}