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
        private IList<Constraint> _constraints;
        private IList<Errand> _errands;
        private IDictionary<CompanyType, IList<Appointment>> _appointmentDataBase;
        private IList<Path> _results;

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
