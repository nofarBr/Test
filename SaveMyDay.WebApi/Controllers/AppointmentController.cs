using SaveMyDate.Entities;
using System;
using System.Collections.Generic;

using System.Web.OData;

using SaveMayDay.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using MongoDB.Bson;

namespace SaveMyDay.WebApi.Controllers
{
    public class AppointmentController : ODataController
    {
        private readonly MongoCrud<Appointment> _mongoCrud;
        public AppointmentController()
        {
            _mongoCrud = new MongoCrud<Appointment>();
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

        [EnableQuery]
        public IQueryable<Appointment> GetAppointment()
        {
            return _mongoCrud.GetAllEntities();
        }


        //public HttpResponseMessage Post(Appointment appointment)
        //{
        //    _mongoCrud.SaveOrUpdate(appointment);
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        [HttpPost]
     
        public IHttpActionResult PostAppointment(ODataActionParameters parameters)
        {
            var appointment = (Appointment)parameters["Appointment"];
            _mongoCrud.SaveOrUpdate(appointment);

            return StatusCode(HttpStatusCode.NoContent);
        }


    }

}

