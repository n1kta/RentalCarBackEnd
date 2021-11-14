using System;

namespace RentalCar.Dtos
{
    public class ReturnDto
    {
        public int RentalId { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool InTime { get; set; }
    }
}