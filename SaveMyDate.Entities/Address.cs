﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDate.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public City City { get; set; }
        public string HouseNumber { get; set; }
    }
}
