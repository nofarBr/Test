using SaveMyDate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SaveMyDay.WebApi.Controllers
{
    public class AppointmentController : ApiController
    {
        public IEnumerable<Appointment> GetAppointmentByCompanyType(CompanyType Type)
        {
            return new List<Appointment> { new Appointment { Code = 1, Company = new Company() { Code = 1, Type = CompanyType.Banks }, Time = DateTime.Now } };
        }

        public IEnumerable<Appointment> GetAllAppointment()
        {
            return new List<Appointment> { new Appointment { Code = 1, Company = new Company() { Code = 1, Type = CompanyType.Banks }, Time = DateTime.Now } };
        }
    }

}

