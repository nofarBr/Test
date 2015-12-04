using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyDate.Entities
{
    // which custumer saved
    public class Appointment
    {
        public int Code { get; set; }
        public Company Company { get; set; }
        public DateTime Time { get; set; }


        public Appointment()
        {
            
     
        }
    }
}
