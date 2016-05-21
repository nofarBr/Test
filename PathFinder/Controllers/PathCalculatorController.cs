using System.Collections.Generic;
using System.Web.Http;
using SaveMyDate.Entities;
using SaveMyDay.Algoritem;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using System;

namespace PathFinder.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PathCalculatorController : ApiController
    {
        [HttpPost]
        public object PathCalculator(JObject jsonParam)
        {
            // Example of parsing paramters.
            //City city = jsonParam["city"].ToObject<City>(); no need fo this....!
            List<Constraint> constraintList = jsonParam["events"].ToObject<List<Constraint>>();
            string city1 = jsonParam["appointmentsCity"].ToObject<string>();
            string travel = jsonParam["travelWay"].ToObject<string>();
            List<JObject> appointmentsJobj = jsonParam["selectedAppointments"].ToObject<List<JObject>>();

            List<string[]> appointments = new List<string[]>();
            foreach (JObject appointment in appointmentsJobj)
            {
                string type = appointment["companyType"].ToObject<String>();
                string subType = appointment["SubType"].ToObject<String>();
                appointments.Add(new string[] { type, subType } );
            }

            //  Constraint cconstraint2 = jsonParam["city2"].ToObject<string>();

            //var companiesForAlgorithem = new ComapanyQueryHandler().GetCompaniesByTypeAndLocation(companyTypes, city);
            //var result = new AlgoritemRunner();
            //result.Activate(companiesForAlgorithem);

            var paths = new List<Path>();

            // Start Test Of Ori // 

            var company_bank1 = new Company()
            {
                Id = "1",
                Location = "אורט 8, אשקלון",
                 
                Type = CompanyType.Banks
            };

            var company_bank2 = new Company()
            {
                Id = "2",
                Location = "נמר 4, אשקלון",
               
                Type = CompanyType.Banks
            };

            var company_clinic1 = new Company()
            {
                Id = "3",
                Location = "לכיש 4, אשקלון",
               
                Type = CompanyType.MedicalClinic
            };

            var company_clinic2 = new Company()
            {
                Id = "4",
                Location = "אילת 2, אשקלון",
               
                Type = CompanyType.MedicalClinic
            };

            var company_post1 = new Company()
            {
                Id = "5",
                Location = "העבודה 2, אשקלון",
               
                Type = CompanyType.PostOffice
            };

            var company_post2 = new Company()
            {
                Id = "6",
                Location = "דוד רמז 8, אשקלון",
               
                Type = CompanyType.PostOffice
            };

            var path_1 = new Path()
            {
                Id = "1",
                Appointments = new List<Appointment>()
                {
                    new Appointment()
                    {
                        Id = "1",
                        Time = new DateTime(2016, 5, 17, 9, 30, 00),
                        Company = company_bank1,
                        Remark = "בנק מזרחי - סניף אשקלון"
                    },
                    new Appointment()
                    {
                        Id = "2",
                        Time = new DateTime(2016, 5, 17, 10, 30, 00),
                        Company = company_clinic1,
                        Remark = "רופא עיניים - ד''ר סימנובסקי"
                    }
                },
                type = TravelWay.Car
            };

            var path_2 = new Path()
            {
                Id = "2",
                Appointments = new List<Appointment>()
                {
                    new Appointment()
                    {
                        Id = "3",
                        Time = new DateTime(2016, 5, 17, 10, 30, 00),
                        Company = company_post2,
                        Remark = "דואר - סניף אשקלון"
                    },
                    new Appointment()
                    {
                        Id = "4",
                        Time = new DateTime(2016, 5, 17, 11, 45, 00),
                        Company = company_bank2,
                        Remark = "בנק מזרחי - סניף אשקלון"
                    }
                },
                type = TravelWay.Car
            };

            var path_3 = new Path()
            {
                Id = "3",
                Appointments = new List<Appointment>()
                {
                    new Appointment()
                    {
                        Id = "5",
                        Time = new DateTime(2016, 5, 17, 8, 45, 00),
                        Company = company_post1,
                        Remark = "דואר - סניף אשקלון"
                    },
                    new Appointment()
                    {
                        Id = "6",
                        Time = new DateTime(2016, 5, 17, 13, 20, 00),
                        Company = company_clinic2,
                        Remark = "רופא עיניים - ד''ר כהן"
                    }
                },
                type = TravelWay.Car
            };

            paths.Add(path_1);
            paths.Add(path_2);
            paths.Add(path_3);

            // End Test Of Ori //

            /*
            var path = new Path();
            path.Id = "1";
            path.type = Path.MovementType.Car;
            paths.Add(path);

            var path2 = new Path();
            path2.Id = "2";
            path2.type = Path.MovementType.walk;
            paths.Add(path2);
            */

            // Send to the client
            var result = new { paths = paths};
            return Json(result);
        }
    }
}
