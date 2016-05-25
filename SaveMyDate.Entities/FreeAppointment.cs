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
        public DateTime StartTime { get; set; }
        public Int32 Duration { get; set; }
        public string Remark { get; set; }

        public FreeAppointment()
        {


        }

        public FreeAppointment(DateTime StartTime, Int32 Duration, string Remark, string Id)
        {
            this.StartTime = StartTime;
            this.Duration = Duration;
            this.Remark = Remark;
            this.Id = Id;
        }
    }
}
