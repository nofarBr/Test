using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaySchedualer.Controllers
{
    public class PathController : ApiController
    {
        [HttpPost]
        public void PostAppointment(int pathId)
        {
            //schedual path & appointment
        }
    }
}
