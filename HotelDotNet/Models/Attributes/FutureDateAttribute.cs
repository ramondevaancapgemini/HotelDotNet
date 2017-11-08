using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime) value;

            if (date > DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date must be in the future");
        }
    }
}
