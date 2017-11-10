using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models
{
    public enum RoomType
    {
        [DisplayName("Luxury")]
        Luxury,
        [DisplayName("Regular")]
        Regular,
        [DisplayName("Budget")]
        Budget
    }
}
