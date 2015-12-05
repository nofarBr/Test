using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SaveMyDate.Entities
{
    // which custumer saved
    public class Appointment
    {
        
        public ObjectId Id { get; set; }
        public Company Company { get; set; }
        public DateTime Time { get; set; }
        public DateTime LastModified { get; set; }
        public string Remark { get; set; }

        public Appointment()
        {
            
     
        }
    }
}
