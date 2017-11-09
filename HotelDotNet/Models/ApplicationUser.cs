using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelDotNet.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string GivenName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string SurnamePrefix { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }

        [NotMapped]
        public string FullName => string.Join(' ', GivenName, SurnamePrefix, Surname);
    }
}
