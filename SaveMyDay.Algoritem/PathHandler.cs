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
        
        public IList<CompanyType> GetErrandsCodes()
        {
            IList<CompanyType> erransdCods = new List<CompanyType>();
            foreach (var appointment in Path.Appointments)
            {
                if(!erransdCods.Contains(appointment.Company.Type))
                    erransdCods.Add(appointment.Company.Type);
            }
            return erransdCods;
        }

        public void AddAppointment(Appointment appointment)
        {
            Path.Appointments.Add(appointment);
        }

        public bool IsAppointmentAddable(Appointment appointment, Dictionary<Tuple<string, string>, int> deltaTimeMatrix)
        {
            //TODO: code
            /* for all app/cons
             * find the on in middle by times
             * check by distance to see if fits V
            */
            return true;
        }
        
        private TimeSpan GetDeadTime(/* app/cons 1, app/cons 2, matrix*/)
        {
            //return appointment.Time - time - matrix(id,id); // TODO: add distance time
            return new TimeSpan();
        }

        public double CalcWatedTimeInSeconds()
        {
            List<PathItemHandler> items = new List<PathItemHandler>();
            Path.Appointments.ForEach(a => items.Add(new PathItemHandler(a)));
            Path.Constraints.ForEach(c => items.Add(new PathItemHandler(c)));
            items = items.OrderBy(i => i.StartTime).ToList<PathItemHandler>();

            double diffInSeconds = 0;

            DateTime before = items[0].StartTime;
            foreach (PathItemHandler item in items)
            {
                diffInSeconds += (item.StartTime - before).TotalMinutes;
                before = item.EndTime;
            }

            return diffInSeconds;
        }
    }
}
