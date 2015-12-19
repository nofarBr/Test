using SaveMyDate.Entities;
using System.Web.OData;
using SaveMayDay.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaveMyDay.WebApi.Controllers
{
    public class AppointmentController : ODataController
    {
        private readonly MongoCrud<Appointment> _mongoCrud;
        public AppointmentController()
        {
            _mongoCrud = new MongoCrud<Appointment>();
        }
       
        [EnableQuery]
        public IQueryable<Appointment> GetAppointment()
        {
            return _mongoCrud.GetAllEntities();
        }

        [HttpPost]
        public HttpResponseMessage PostAppointment([FromBody]Appointment appointment)
        {
            _mongoCrud.SaveOrUpdate(appointment);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // odata other try
        //[HttpPost]
        //public IHttpActionResult Post([FromBody]Appointment appointment)
        //{
        //    _mongoCrud.SaveOrUpdate(appointment);
        //    return StatusCode(HttpStatusCode.OK);
        //}

        // One try of odata
        //public IHttpActionResult PostAppointment(ODataActionParameters parameters)
        //{
        //    var appointment = (Appointment)parameters["Appointment"];
        //    _mongoCrud.SaveOrUpdate(appointment);

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


    }

}

