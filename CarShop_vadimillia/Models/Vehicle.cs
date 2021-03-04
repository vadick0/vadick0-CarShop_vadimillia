using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop_vadimillia.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public int BluidYear { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }
    }
}
