using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models
{
    public enum Gender
    {
        [DisplayName("Male")]
        Male,
        [DisplayName("Female")]
        Female
    }
}
