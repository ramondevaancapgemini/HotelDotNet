using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models.ViewModels
{
    public class BookingUserViewModel
    {
        public long Id { get; set; }
        [DisplayName("From")]
        public DateTime From { get; set; }
        [DisplayName("To")]
        public DateTime To { get; set; }

        [DisplayName("Room")]
        public Room Room { get; set; }
        [Required]
        [DisplayName("RoomId")]
        public long RoomId { get; set; }

        [DisplayName("User")]
        public UserViewModel User { get; set; }
    }
}
