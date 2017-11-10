using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models
{
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Booking")]
        public Booking Booking { get; set; }
        [Required]
        [DisplayName("Booking id")]
        public long BookingId { get; set; }
    }
}
