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
            var userA = userManager.FindByEmailAsync("a@a.a").Result;
            if (userA == null)
            {
                userA = new ApplicationUser()
                {
                    UserName = "a@a.a",
                    Email = "a@a.a",
                    GivenName = "UserA",
                    City = "ACity",
                    Country = "ACountry",
                    Street = "AStreet",
                    Gender = Gender.Female,
                    SurnamePrefix = "van",
                    Surname = "Aa",
                    PostalCode = "1111AA"
                };
                var result = userManager.CreateAsync(userA, "usera").Result;
                result = userManager.SetLockoutEnabledAsync(userA, false).Result;

                userManager.AddToRoleAsync(userA, guestRole.Name).Wait();
            }
            var userB = userManager.FindByEmailAsync("b@b.b").Result;
            if (userB == null)
            {
                userB = new ApplicationUser()
                {
                    UserName = "b@b.b",
                    Email = "b@b.b",
                    GivenName = "UserB",
                    City = "BCity",
                    Country = "BCountry",
                    Street = "BStreet",
                    Gender = Gender.Male,
                    SurnamePrefix = "van",
                    Surname = "Beeb",
                    PostalCode = "9999BB"
                };
                var result = userManager.CreateAsync(userB, "userb").Result;
                result = userManager.SetLockoutEnabledAsync(userB, false).Result;

                userManager.AddToRoleAsync(userB, guestRole.Name).Wait();
            }
        }
    }
}
