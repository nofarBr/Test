using Newtonsoft.Json.Linq;
using SaveMyDate.Entities;
using System.Web.Http;
using System.Web.Http.Cors;

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
        }
    }
}
