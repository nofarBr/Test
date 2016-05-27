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
            IEnumerable<object> sorted = Path.Appointments.Concat<object>(Path.Constraints)
               .OrderBy(n => n is Appointment ? ((Appointment)n).Time : ((Constraint)n).StartTime);

            double diffInSeconds = 0;

            DateTime before = DateTime.MinValue;
            foreach (object either in sorted)
            {
                if (either is Appointment)
                {
                    if (before == DateTime.MinValue)
                    {
                        before = ((Appointment)either).Time.AddHours(1);
                    }
                    else
                    {
                        diffInSeconds += (((Appointment)either).Time - before).TotalMinutes;
                        before = ((Appointment)either).Time.AddHours(1);
                    }
                }
                else
                {
                    if (before == DateTime.MinValue)
                    {
                        before = ((Constraint)either).EndTime;
                    }
                    else
                    {
                        diffInSeconds += (((Constraint)either).StartTime - before).TotalMinutes;
                        before = ((Constraint)either).EndTime;
                    }
                }
            }

            return diffInSeconds;
        }
    }
}
