using Newtonsoft.Json.Linq;
using SaveMyDate.Entities;
using System.Web.Http;
using System.Web.Http.Cors;
using CompanySimulator;

namespace DaySchedualer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PathController : ApiController
    {
        [HttpPost]
        public void PostAppointment(JObject jsonParam)
        {
            //schedual path & appointment
            var selectedPath = jsonParam.ToObject<Path>();
            var appointmentSchedualer = new AppointmentSchedualer();

            foreach (var appointment in selectedPath.Appointments)
            {
                appointmentSchedualer.SchedualAppointment(appointment);
            }
        }
    }
}
