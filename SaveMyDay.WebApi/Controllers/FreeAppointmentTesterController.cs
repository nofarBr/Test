using System.Collections.Generic;
using SaveMyDate.Entities;
using System.Web.Http;
using SaveMayDay.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.OData;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;
using System;
using MongoDB.Bson;

namespace SaveMyDay.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FreeAppointmentTesterController : ApiController
    {
        private readonly MongoCrud<FreeAppointmentCompany> _mongoCrud;
        private readonly MongoCrud<FreeAppointment> _mongoCrudFA;
        private readonly MongoCrud<Company> _mongoCrudCompany;
        public FreeAppointmentTesterController()
        {
            _mongoCrud = new MongoCrud<FreeAppointmentCompany>();
            _mongoCrudCompany = new MongoCrud<Company>();
            _mongoCrudFA = new MongoCrud<FreeAppointment>();
        }

        [HttpPost]
        public FreeAppointmentCompany PostCompany(JObject jsonParam)
        {
            Company company = _mongoCrudCompany.GetEntityById(jsonParam.GetValue("companyId").ToString());
            FreeAppointmentCompany fac = new FreeAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            DateTime testDate = DateTime.Now.Date;
            testDate.AddHours(6);

            DateTime endDate = testDate.AddYears(1);

            List<string> times = new List<string>();

            while (testDate.ToString() != endDate.ToString())
            {
                Console.WriteLine(testDate.ToString());
                fac.freeAppointments.Add(new FreeAppointment(testDate, 30, "בנקאי", ObjectId.GenerateNewId().ToString()));
                times.Add(testDate.ToString("HH:mm:ss"));
                testDate = testDate.AddMinutes(30);
                
                if (testDate.Hour == 21) { testDate.AddHours(9); }
            }

            //fac.freeAppointments;

            foreach (FreeAppointment fa in fac.freeAppointments)
            {
                _mongoCrudFA.SaveOrUpdate(fa);
            }
            _mongoCrud.SaveOrUpdate(fac);
            return fac;
        }
    }
}
