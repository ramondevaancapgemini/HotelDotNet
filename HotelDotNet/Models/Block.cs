﻿using System;
using System.Collections.Generic;
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
        public DateTime From { get; set; }
        [Required]
        [DateAfter("From")]
        public DateTime To { get; set; }
        [Required]
        public string Description { get; set; }

        public Room Room { get; set; }
        [Required]
        public long RoomId { get; set; }
    }
}
