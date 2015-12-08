using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace SaveMyDay.Algoritem
{
    public class Algoritem
    {
        private IList<Constraint> _constraints { get; set; }
        private IList<Errand> _errands { get; set; }
        private IDictionary<CompanyType, IList<Appointment>> _appointmentDataBase { get; set; }
        private IList<Path> _results { get; set; }

        public Algoritem()
        {
            _constraints = new List<Constraint>();
            _errands = new List<Errand>();
        }

        public bool Activate()
        {
            return false;
        }

        public Path GetResult(int priority)
        {
            return null;
        }
    }
}
