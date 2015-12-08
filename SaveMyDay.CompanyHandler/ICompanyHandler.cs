using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace SaveMyDay.CompanyHandler
{
    public interface ICompanyHandler
    {
        IList<Appointment> GetFreeAppointments(string url);
        bool SetAppointment(string url, Appointment appointment);
    }
}
