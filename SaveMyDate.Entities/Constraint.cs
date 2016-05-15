using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyDate.Entities
{
    public class Constraint : TimeWindow
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }


    }
}
