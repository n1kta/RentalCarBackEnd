using System.Collections.Generic;
using RentalCar.Models;

namespace RentalCar.Entity
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
