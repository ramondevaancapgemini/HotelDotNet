using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models.Attributes
{
    public class DateAfterAttribute : ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    DateTime end = (DateTime) value;
        //}

        public DateAfterAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }
        
        public string OtherProperty { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime) value;

            if (date > ((DateTime) validationContext.ObjectType
                .GetProperty(OtherProperty)
                .GetValue(validationContext.ObjectInstance, null)))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date was incorrect");
        }
    }
}
