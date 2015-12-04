using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyDay.Entities
{
    public class Constraint : TimeWindow
    {
        public int Code { get; set; }
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
    }
}
