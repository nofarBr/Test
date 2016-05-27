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
        private readonly MongoCrud<DbAppointmentCompany> _mongoCrud;
        private readonly MongoCrud<DbAppointment> _mongoCrudFA;
        private readonly MongoCrud<Company> _mongoCrudCompany;
        public FreeAppointmentTesterController()
        {
            _mongoCrud = new MongoCrud<DbAppointmentCompany>();
            _mongoCrudCompany = new MongoCrud<Company>();
            _mongoCrudFA = new MongoCrud<DbAppointment>();
        }

        [HttpPost]
        public DbAppointmentCompany PostCompany()
        {
            List<string> locs = new List<string>();
            locs.Add("‏רוטשילד 78, ראשון לציון");
            locs.Add("מורשת ישראל 15, ראשון לציון");
            locs.Add("‏ההסתדרות 36, חולון");
            locs.Add("חנקין 48, חולון");
            locs.Add("הרצל 18, אשקלון");
            locs.Add("ההסתדרות 40, אשקלון");
            locs.Add("הרצל 20, גן יבנה");
            locs.Add("המגינים 56, גן יבנה");

            foreach (string loc in locs)
            {
                Company company = new Company();
                company.Id = ObjectId.GenerateNewId().ToString();
                company.Location = loc;
                company.SubType = "בנק הפועלים";
                company.Type = CompanyType.PostOffice;
                company.UrlForApi = "http://some.uri.com/";
                _mongoCrudCompany.SaveOrUpdate(company);

                DbAppointmentCompany fac = new DbAppointmentCompany();
                fac.Company = company;
                fac.Id = ObjectId.GenerateNewId().ToString();

                DateTime testDate = DateTime.Now.Date;
                testDate.AddHours(6);

                DateTime endDate = testDate.AddMonths(2);

                List<string> times = new List<string>();

                while (testDate.ToString() != endDate.ToString())
                {
                    Console.WriteLine(testDate.ToString());
                    fac.freeAppointments.Add(new DbAppointment(testDate, 30, "בנקאי", ObjectId.GenerateNewId().ToString()));
                    times.Add(testDate.ToString("HH:mm:ss"));
                    testDate = testDate.AddMinutes(30);

                    if (testDate.Hour == 21) { testDate.AddHours(9); }
                }

                _mongoCrud.SaveOrUpdate(fac);
            }
            return null;
        }
    }
}
