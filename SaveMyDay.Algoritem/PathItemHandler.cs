using SaveMyDate.Entities;
using System;

namespace SaveMyDay.Algoritem
{
    public class PathItemHandler
    {
        public PathItemHandler(Appointment app)
        {
            StartTime = app.Time;
            EndTime = app.Time.AddHours(1);
            Type = PathItemType.Appointment;
            Id = app.Company.Id;
        }

        public PathItemHandler(Constraint con)
        {
            StartTime = con.StartTime;
            EndTime = con.EndTime;
            Type = PathItemType.Constraint;
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Id { get; private set; }
        public PathItemType Type { get; private set; }
        
    }

    public enum PathItemType
    {
        Appointment,
        Constraint
    }


}
