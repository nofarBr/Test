using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace SaveMyDay.Algoritem
{
    public class AlgoritemRunner
    {
        public IList<Constraint> Constraints { get; private set; }
        public IList<Errand> Errands { get; private set; }
        public IDictionary<CompanyType, IList<Appointment>> AppointmentDataBase { get; private set; }
        public IList<Path> Results { get; private set; }

        public AlgoritemRunner()
        {
            AppointmentDataBase = new Dictionary<CompanyType, IList<Appointment>>();
            Results = new List<Path>();
            Constraints = new List<Constraint>();
            Errands = new List<Errand>();
        }

        public bool Activate(List<Company> companies)
        {
            return false;
        }
    }
}
