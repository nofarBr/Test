using SaveMyDate.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.OData;
using SaveMayDay.Common;
using System.Linq;
using MongoDB.Bson;

namespace SaveMyDay.WebApi.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly MongoCrud<Appointment> _mongoDbHandler;
        public AppointmentController()
        {
            _mongoDbHandler = new MongoCrud<Appointment>();
        }
        public IEnumerable<Appointment> GetAppointmentByCompanyType(string Type)
        {
            CompanyType CompanyType;
            if (Enum.TryParse(Type, true, out CompanyType))
            {
                return new List<Appointment> { new Appointment { Company = new Company() { Type = CompanyType.Banks }, Time = DateTime.Now } };
            }
            else
            {
                return new List<Appointment>();
            }
        }

        [EnableQueryAttribute]
        public IQueryable<Appointment> GetAllAppointment()
        {
            return _mongoDbHandler.GetEntity();
        }

        [HttpPost]
        public void PostAppointment(Appointment appointment)
        {
            _mongoDbHandler.SaveOrUpdate(appointment);
        }

       
    }

}

