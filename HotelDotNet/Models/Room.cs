﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Room type")]
        public RoomType RoomType { get; set; }
        [Required]
        [Display(Name = "Room size")]
        public RoomSize RoomSize { get; set; }

        [DisplayName("Bookings")]
        public IEnumerable<Booking> Bookings { get; set; }
        [DisplayName("Blocks")]
        public IEnumerable<Block> Blocks { get; set; }
    }
}
