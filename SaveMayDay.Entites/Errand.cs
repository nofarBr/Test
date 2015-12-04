using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyDay.Entities
{
    public class Errand : TimeWindow
    {
        public int Code { get; set; }
        public Location Location { get; set; }
        //public CompanyType CompanyType { get; set; }

    }
}
