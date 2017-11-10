using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models
{
    public enum RoomSize
    {
        [DisplayName("One person")]
        OnePerson,
        [DisplayName("Two person")]
        TwoPerson,
        [DisplayName("Three/four person")]
        ThreeFourPerson,
        [DisplayName("Five/six person")]
        FiveSixPerson
    }
}
