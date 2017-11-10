using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Gender")]
        public Gender Gender { get; set; }
        [Required]
        [DisplayName("Given name")]
        public string GivenName { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Surname prefix")]
        public string SurnamePrefix { get; set; }

        [Required]
        [DisplayName("Street")]
        public string Street { get; set; }
        [Required]
        [DisplayName("City")]
        public string City { get; set; }
        [Required]
        [DisplayName("Postal code")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }

        [NotMapped]
        [DisplayName("Full name")]
        public string FullName => string.Join(' ', GivenName, SurnamePrefix, Surname);
    }
}
