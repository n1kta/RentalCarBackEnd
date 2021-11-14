using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RentalCar.Enums;
using RentalCar.Models;

namespace RentalCar.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; }
    }
}
