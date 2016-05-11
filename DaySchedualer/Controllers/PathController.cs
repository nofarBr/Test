using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace DaySchedualer.Controllers
{
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
