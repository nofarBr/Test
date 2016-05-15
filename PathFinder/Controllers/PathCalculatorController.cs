using System.Collections.Generic;
using System.Web.Http;
using SaveMyDate.Entities;
using SaveMyDay.Algoritem;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;

namespace PathFinder.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PathCalculatorController : ApiController
    {
        [HttpPost]
        public object PathCalculator(JObject jsonParam)
        {
            // Example of parsing paramters.
            City city = jsonParam["city"].ToObject<City>();
            //List<Constraint> constraintList = jsonParam["events"].ToObject<List<Constraint>>();
            //City city1 = jsonParam["appointmentsCity"].ToObject<City>();
            //List<string> appointmentsIds = jsonParam["selectedAppointments"].ToObject<List<string>>();
           
            //  Constraint cconstraint2 = jsonParam["city2"].ToObject<string>();

            //var companiesForAlgorithem = new ComapanyQueryHandler().GetCompaniesByTypeAndLocation(companyTypes, city);
            //var result = new AlgoritemRunner();
            //result.Activate(companiesForAlgorithem);

            var paths = new List<Path>();
            var path = new Path();
            path.Id = "1";
            path.type = Path.MovementType.Car;
            paths.Add(path);

            var path2 = new Path();
            path2.Id = "2";
            path2.type = Path.MovementType.walk;
            paths.Add(path2);

            // Send to the client
            var result = new { paths = paths, city = city };
            return Json(result);
        }
    }
}
