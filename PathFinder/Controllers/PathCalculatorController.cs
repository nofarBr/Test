using System.Collections.Generic;
using System.Web.Http;
using SaveMyDate.Entities;
using SaveMyDay.Algoritem;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using CompanySimulator;
using SaveMyDay.DistancesMatrix;

namespace PathFinder.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PathCalculatorController : ApiController
    {
        [HttpPost]
        public object PathCalculator(JObject jsonParam)
        {
            // Example of parsing paramters.
            List<Constraint> constraintList = jsonParam["events"].ToObject<List<Constraint>>();
            string city = jsonParam["appointmentsCity"].ToObject<string>();
            string travel = jsonParam["travelWay"].ToObject<string>();
            DateTime selectedDate = jsonParam["selectedDate"].ToObject<DateTime>();
            List<JObject> appointmentsJobj = jsonParam["selectedAppointments"].ToObject<List<JObject>>();

            List<string[]> errands = new List<string[]>();
            List<CompanySubType> errandsForAlgo = new List<CompanySubType>();
            foreach (JObject appointment in appointmentsJobj)
            {
                string type = appointment["companyType"].ToObject<String>();
                string subType = appointment["SubType"].ToObject<String>();
                errands.Add(new string[] { type, subType } );
                errandsForAlgo.Add((CompanySubType)Enum.Parse(typeof(CompanySubType), subType));
            }

            // this is the call to company simulator for dror, then is the call to algorithem,need to fill up parameters.
            var appointments = FindAppointments(errands, selectedDate, city);
            var matrixDictionary = DistancesMatrixReader.Read();
            var algoritemRunner = new AlgoritemRunner();
            algoritemRunner.Activate(errandsForAlgo, constraintList, appointments, matrixDictionary);

            List<Path> resultPaths = algoritemRunner.Results.ToList().GetRange(0, 3);

            var paths = new List<Path>();

           // Send to the client
            var result = new { paths = resultPaths };
            return Json(result);
        }

        private Dictionary<CompanySubType, List<Appointment>> FindAppointments(List<string[]> errands, DateTime selectedDate, string citySelected)
        {
            var freeAppointmentFinder = new FreeAppointmentFinder();
            var dbCompanyList = new List<DbAppointmentCompany>();

            foreach (string[] errand in errands)
            {
                dbCompanyList.AddRange(freeAppointmentFinder.FindFreeAppointmentByDay(selectedDate, (CompanyType) Enum.Parse(typeof(CompanyType), errand[0]), (CompanySubType)Enum.Parse(typeof(CompanySubType), errand[1]), citySelected));
            }

            return dbCompanyList.ToDictionary(dbAppointmentCompany => dbAppointmentCompany.Company.SubType, dbAppointmentCompany => dbAppointmentCompany.ConvertToAppointments());
        }
    }
}
