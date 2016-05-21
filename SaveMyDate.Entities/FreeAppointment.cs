using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDate.Entities
{
    public class FreeAppointment : IMongoEntity
    {
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public Int32 Duration { get; set; }
        public string Remark { get; set; }

        public FreeAppointment()
        {


        }
    }
}
