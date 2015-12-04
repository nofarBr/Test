using System;

namespace SaveMyDate.Entities
{
    public abstract class TimeWindow
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
