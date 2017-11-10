using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelDotNet.Models.Attributes;

namespace HotelDotNet.Models
{
    public class Block
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("From")]
        public DateTime From { get; set; }
        [Required]
        [DateAfter("From")]
        [DisplayName("To")]
        public DateTime To { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Room")]
        public Room Room { get; set; }
        [Required]
        [DisplayName("Room id")]
        public long RoomId { get; set; }
    }
}
