using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDotNet.Models.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}
