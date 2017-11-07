using System;
using System.Collections.Generic;
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
        public DateTime Date { get; set; }

        public Booking Booking { get; set; }
        [Required]
        public long BookingId { get; set; }
    }
}
