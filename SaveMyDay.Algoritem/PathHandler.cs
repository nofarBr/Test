using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMyDate.Entities;

namespace SaveMyDay.Algoritem
{
    class PathHandler
    {
        public Path Path { get; private set; }

        public PathHandler(Path path)
        {
            Path = path;
        }

        public PathHandler Clone()
        {
            var path = new Path();
            path.Appointments.AddRange(Path.Appointments);
            path.Constraints.AddRange(Path.Constraints);
            path.type = Path.type;
            return new PathHandler(path);
        }
        
        public IList<CompanySubType> GetErrandsCodes()
        {
            IList<CompanySubType> erransdCods = new List<CompanySubType>();
            foreach (var appointment in Path.Appointments)
            {
                if(!erransdCods.Contains(appointment.Company.SubType))
                    erransdCods.Add(appointment.Company.SubType);
            }
            return erransdCods;
        }

        public void AddAppointment(Appointment appointment)
        {
            Path.Appointments.Add(appointment);
        }

        public bool IsAppointmentAddable(Appointment appointment, Dictionary<Tuple<string, string>, int> deltaTimeMatrix)
        {
            List<PathItemHandler> items = new List<PathItemHandler>();
            Path.Appointments.ForEach(a => items.Add(new PathItemHandler(a)));
            for (int i = 0; i < Path.Constraints.Count; i++)
            {
                items.Add(new PathItemHandler(Path.Constraints[i], i));
            }
            //Path.Constraints.ForEach(c => items.Add(new PathItemHandler(c)));
            items = items.OrderBy(i => i.StartTime).ToList<PathItemHandler>();
            PathItemHandler lastItem = null;
            foreach (var item in items)
            {
                if (item.StartTime > appointment.Time)
                {
                    if (GetDeadTime(new PathItemHandler(appointment), item, deltaTimeMatrix) >= new TimeSpan())
                    {
                        if (lastItem == null || GetDeadTime(lastItem, new PathItemHandler(appointment), deltaTimeMatrix) > new TimeSpan())
                            return true;
                    }
                    return false;
                }
                else if (item.EndTime > appointment.Time)
                    return false;
                lastItem = item;
            }
            return true;
        }
        
        private TimeSpan GetDeadTime(PathItemHandler first, PathItemHandler second, Dictionary<Tuple<string, string>, int> deltaTimeMatrix)
        {
            return second.StartTime - first.EndTime - new TimeSpan(0,0,deltaTimeMatrix[new Tuple<string,string>(first.Id, second.Id)]);
        }

        public double GetOverallDeadTime(Dictionary<Tuple<string, string>, int> deltaTimeMatrix)
        {
            List<PathItemHandler> items = new List<PathItemHandler>();
            Path.Appointments.ForEach(a => items.Add(new PathItemHandler(a)));
            for (int i = 0; i < Path.Constraints.Count; i++)
            {
                items.Add(new PathItemHandler(Path.Constraints[i], i));
            }
            //Path.Constraints.ForEach(c => items.Add(new PathItemHandler(c)));
            items = items.OrderBy(i => i.StartTime).ToList<PathItemHandler>();

            double diffInMinutes = 0;

            PathItemHandler before = items[0];
            for (var i = 1; i < items.Count; i++)
            {
                diffInMinutes += GetDeadTime(before, items[i], deltaTimeMatrix).TotalMinutes;
                before = items[i];
            }

            return diffInMinutes;
        }

        public double CalcWatedTimeInSeconds()
        {
            List<PathItemHandler> items = new List<PathItemHandler>();
            Path.Appointments.ForEach(a => items.Add(new PathItemHandler(a)));
            for (int i = 0; i < Path.Constraints.Count; i++)
            {
                items.Add(new PathItemHandler(Path.Constraints[i], i));
            }
            //Path.Constraints.ForEach(c => items.Add(new PathItemHandler(c)));
            items = items.OrderBy(i => i.StartTime).ToList<PathItemHandler>();

            double diffInSeconds = 0;

            if (items.Count != 0)
            {
                DateTime before = items[0].StartTime;
                foreach (PathItemHandler item in items)
                {
                    diffInSeconds += (item.StartTime - before).TotalMinutes;
                    before = item.EndTime;
                }
            }

            return diffInSeconds;
        }
    }
}
