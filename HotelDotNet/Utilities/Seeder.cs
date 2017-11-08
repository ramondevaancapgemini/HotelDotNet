using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelDotNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelDotNet.Utilities
{
    public static class Seeder
    {

        public static void Initialize(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            // check for roles 
            var adminRole = roleManager.FindByNameAsync("Admin").Result;
            if (adminRole == null)
            {
                adminRole = new IdentityRole("Admin");
                roleManager.CreateAsync(adminRole).Wait();
            }
            var receptionistRole = roleManager.FindByNameAsync("Receptionist").Result;
            if (receptionistRole == null)
            {
                receptionistRole = new IdentityRole("Receptionist");
                roleManager.CreateAsync(receptionistRole).Wait();
            }
            var guestRole = roleManager.FindByNameAsync("Guest").Result;
            if (guestRole == null)
            {
                guestRole = new IdentityRole("Guest");
                roleManager.CreateAsync(guestRole).Wait();
            }

            var adminUser = userManager.FindByNameAsync("admin@admin.admin").Result;
            if (adminUser == null)
            {
                adminUser = new ApplicationUser() {
                    UserName = "admin@admin.admin",
                    Email = "admin@admin.admin",
                    GivenName = "Admin",
                    City = "AdminCity",
                    Country = "AdminCountry",
                    Street = "AdminStreet",
                    Gender = Gender.Male,
                    SurnamePrefix = "",
                    Surname = "Admin",
                    PostalCode = "1234AD"
                };
                var result = userManager.CreateAsync(adminUser, "admin").Result;
                result = userManager.SetLockoutEnabledAsync(adminUser, false).Result;

                userManager.AddToRoleAsync(adminUser, adminRole.Name).Wait();
                userManager.AddToRoleAsync(adminUser, receptionistRole.Name).Wait();
            }
        }
    }
}
