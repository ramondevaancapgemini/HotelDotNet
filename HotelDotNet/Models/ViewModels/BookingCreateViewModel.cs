using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelDotNet.Models.Attributes;

namespace HotelDotNet.Models
{
    public class BookingCreateViewModel
    {
        public long Id { get; set; }
        [Required]
        [FutureDate]
        [DisplayName("From")]
        public DateTime From { get; set; }
        [Required]
        [FutureDate]
        [DateAfter("From")]
        [DisplayName("To")]
        public DateTime To { get; set; }

        [Required]
        [DisplayName("Room size")]
        public RoomSize RoomSize { get; set; }
        [Required]
        [DisplayName("Room type")]
        public RoomType RoomType { get; set; }
    }
}
