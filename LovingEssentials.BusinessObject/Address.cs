﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.BusinessObject
{
    public class Address
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string Street {  get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public User Users { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
