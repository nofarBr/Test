using SaveMyDate.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.OData;
using MongoDB.Driver;
using SaveMayDay.Common;
using System.Linq;
using MongoDB.Bson;

namespace SaveMyDay.WebApi.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly MongoDbHandler<Appointment> _MongoDbHandler;
        public AppointmentController()
        {
            _MongoDbHandler = new MongoDbHandler<Appointment>();
        }
        public IEnumerable<Appointment> GetAppointmentByCompanyType(string Type)
        {
            CompanyType CompanyType;
            if (Enum.TryParse(Type, true, out CompanyType))
            {
                return new List<Appointment> { new Appointment { Company = new Company() { Code = 1, Type = CompanyType.Banks }, Time = DateTime.Now } };
            }
            else
            {
                return new List<Appointment>();
            }
        }

        [EnableQueryAttribute]
        public IQueryable<Appointment> GetAllAppointment()
        {
            return _MongoDbHandler.Collection.FindAll().AsQueryable();
        }

        [HttpPost]
        public Appointment PostAppointment(Appointment Appointment)
        {
            Appointment.Id = ObjectId.GenerateNewId();
            Appointment.LastModified = DateTime.UtcNow;
            _MongoDbHandler.Collection.Save(Appointment);
            return Appointment;
        }

       
    }

}

