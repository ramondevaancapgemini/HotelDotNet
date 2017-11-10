using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelDotNet.Models.Attributes;

namespace HotelDotNet.Models.ViewModels
{
    public class BookingViewModel
    {
        public long Id { get; set; }
        [DisplayName("From")]
        public DateTime From { get; set; }
        [DisplayName("To")]
        public DateTime To { get; set; }

        [DisplayName("Room")]
        public Room Room { get; set; }
        [Required]
        [DisplayName("Room id")]
        public long RoomId { get; set; }
    }
}
