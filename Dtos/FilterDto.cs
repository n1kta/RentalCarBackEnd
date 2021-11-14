using System;

namespace RentalCar.Dtos
{
    public class FilterDto
    {
        public DateTime? PickUpDate { get; set; }
        public DateTime? DropOffDate { get; set; }
    }
}