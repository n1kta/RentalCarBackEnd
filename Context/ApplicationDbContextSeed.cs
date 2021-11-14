using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RentalCar.Constants;
using RentalCar.Entity;
using RentalCar.Enums;

namespace RentalCar.Context
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(nameof(Role.Admin)));
            await roleManager.CreateAsync(new IdentityRole(nameof(Role.Customer)));
            //Seed Default User
            var defaultUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, nameof(Role.Admin));
            }
        }
    }
}
