using Newtonsoft.Json.Linq;
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
            var pathId = jsonParam["pathId"].ToObject<int>();
        }
    }
}
