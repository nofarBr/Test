using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyDay.Entities
{
    public abstract class TimeWindow
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
