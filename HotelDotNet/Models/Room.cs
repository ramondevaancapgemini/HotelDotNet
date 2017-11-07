using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models
{
    public class Room
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public RoomType RoomType { get; set; }
        [Required]
        public RoomSize RoomSize { get; set; }
    }
}
