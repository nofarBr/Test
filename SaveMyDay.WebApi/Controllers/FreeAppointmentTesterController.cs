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
            locs.Add("‏חכמי קירואן 10, תל אביב");

            /* MEDICS START */
            foreach (string loc in locs)
            {
                Company company1 = new Company();
                company1.Id = ObjectId.GenerateNewId().ToString();
                company1.Location = loc;
                company1.SubType = CompanySubType.ChildsDoctor;
                company1.Type = CompanyType.MedicalClinic;
                company1.UrlForApi = "http://some.uri.com/";
                _mongoCrudCompany.SaveOrUpdate(company1);

                DbAppointmentCompany fac1 = new DbAppointmentCompany();
                fac1.Company = company1;
                fac1.Id = ObjectId.GenerateNewId().ToString();

                DateTime testDate1 = DateTime.Now.Date;
                testDate1 = testDate1.AddHours(17);

                DateTime endDate1 = testDate1.AddMonths(1);

                List<string> times1 = new List<string>();
                while (testDate1.ToString() != endDate1.ToString())
                {
                    fac1.freeAppointments.Add(new DbAppointment(testDate1, 30, "", ObjectId.GenerateNewId().ToString()));
                    testDate1 = testDate1.AddDays(1);
                }

                _mongoCrud.SaveOrUpdate(fac1);
            }


            locs = new List<string>();
            locs.Add("‏ז'בוטינסקי 20, תל אביב");
            locs.Add("‏דפנה 5, תל אביב");
            locs.Add("‏כרמלית 5, תל אביב");
            
            Company company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = "‏ז'בוטינסקי 20, תל אביב";
            company.SubType = CompanySubType.FamilyDoctor;
            company.Type = CompanyType.MedicalClinic;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            Company company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = "‏דפנה 5, תל אביב";
            company2.SubType = CompanySubType.FamilyDoctor;
            company2.Type = CompanyType.MedicalClinic;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            Company company3 = new Company();
            company3.Id = ObjectId.GenerateNewId().ToString();
            company3.Location = "‏כרמלית 5, תל אביב";
            company3.SubType = CompanySubType.FamilyDoctor;
            company3.Type = CompanyType.MedicalClinic;
            company3.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company3);

            DbAppointmentCompany fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            DbAppointmentCompany fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();

            DbAppointmentCompany fac3 = new DbAppointmentCompany();
            fac3.Company = company3;
            fac3.Id = ObjectId.GenerateNewId().ToString();

            DateTime testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(21);
            DateTime testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(19);
            testDate2 = testDate2.AddMinutes(30);
            DateTime testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(15);
            DateTime testDate4 = DateTime.Now.Date;
            testDate4 = testDate4.AddHours(23);
            DateTime testDate5 = DateTime.Now.Date;
            DateTime testDate6 = DateTime.Now.Date;
            DateTime testDate7 = DateTime.Now.Date;

            DateTime endDate = testDate.AddMonths(1);

            List<string> times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac3.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                fac3.freeAppointments.Add(new DbAppointment(testDate4, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);
            _mongoCrud.SaveOrUpdate(fac3);





            locs = new List<string>();
            locs.Add("‏ז'בוטינסקי 10, תל אביב");
            locs.Add("‏וייצמן 30, תל אביב");
            locs.Add("‏כרמלית 50, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.SkinDoctor;
            company.Type = CompanyType.MedicalClinic;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = locs[1];
            company2.SubType = CompanySubType.SkinDoctor;
            company2.Type = CompanyType.MedicalClinic;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            company3 = new Company();
            company3.Id = ObjectId.GenerateNewId().ToString();
            company3.Location = locs[2];
            company3.SubType = CompanySubType.SkinDoctor;
            company3.Type = CompanyType.MedicalClinic;
            company3.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company3);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();

            fac3 = new DbAppointmentCompany();
            fac3.Company = company3;
            fac3.Id = ObjectId.GenerateNewId().ToString();

            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(15);
            testDate = testDate.AddMinutes(15);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(18);
            testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(20);
            testDate3 = testDate3.AddMinutes(30);
            testDate4 = DateTime.Now.Date;
            testDate4 = testDate4.AddHours(23);
            testDate4 = testDate4.AddMinutes(55);
            testDate5 = DateTime.Now.Date;
            testDate5 = testDate5.AddHours(18);
            testDate5 = testDate5.AddHours(5);
            testDate6 = DateTime.Now.Date;
            testDate6 = testDate6.AddHours(19);
            testDate6 = testDate6.AddHours(30);
            testDate7 = DateTime.Now.Date;
            testDate7 = testDate7.AddHours(18);
            testDate7 = testDate7.AddHours(30);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                fac.freeAppointments.Add(new DbAppointment(testDate4, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate5, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate6, 30, "", ObjectId.GenerateNewId().ToString()));
                fac3.freeAppointments.Add(new DbAppointment(testDate7, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);
            _mongoCrud.SaveOrUpdate(fac3);






            /* BANKS START */
            locs = new List<string>();
            locs.Add("‏הגפן 1, תל אביב");
            locs.Add("‏דניאל 27, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.BankDiscount;
            company.Type = CompanyType.Banks;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = locs[1];
            company2.SubType = CompanySubType.BankDiscount;
            company2.Type = CompanyType.Banks;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();

            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(13);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(23);
            testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(17);
            testDate4 = DateTime.Now.Date;
            testDate4 = testDate4.AddHours(23);
            testDate4 = testDate4.AddMinutes(55);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate4, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);







            locs = new List<string>();
            locs.Add("‏בן יהודה 200, תל אביב");
            locs.Add("‏נהרדעא 16, תל אביב");
            locs.Add("‏הכובשים 62, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.BankLeumi;
            company.Type = CompanyType.Banks;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = locs[1];
            company2.SubType = CompanySubType.BankLeumi;
            company2.Type = CompanyType.Banks;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            company3 = new Company();
            company3.Id = ObjectId.GenerateNewId().ToString();
            company3.Location = locs[2];
            company3.SubType = CompanySubType.BankLeumi;
            company3.Type = CompanyType.Banks;
            company3.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company3);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();

            fac3 = new DbAppointmentCompany();
            fac3.Company = company3;
            fac3.Id = ObjectId.GenerateNewId().ToString();

            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(19);
            testDate = testDate.AddMinutes(55);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(19);
            testDate2 = testDate2.AddMinutes(30);
            testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(21);
            testDate3 = testDate3.AddMinutes(55);
            testDate4 = DateTime.Now.Date;
            testDate4 = testDate4.AddHours(18);
            testDate4 = testDate4.AddMinutes(5);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                fac3.freeAppointments.Add(new DbAppointment(testDate4, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);
            _mongoCrud.SaveOrUpdate(fac3);







            locs = new List<string>();
            locs.Add("‏יפה נוף 1, תל אביב");
            locs.Add("‏בארי 38, תל אביב");
            locs.Add("‏הכרמל 12, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.BankMizrahi;
            company.Type = CompanyType.Banks;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = locs[1];
            company2.SubType = CompanySubType.BankMizrahi;
            company2.Type = CompanyType.Banks;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            company3 = new Company();
            company3.Id = ObjectId.GenerateNewId().ToString();
            company3.Location = locs[2];
            company3.SubType = CompanySubType.BankMizrahi;
            company3.Type = CompanyType.Banks;
            company3.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company3);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();

            fac3 = new DbAppointmentCompany();
            fac3.Company = company3;
            fac3.Id = ObjectId.GenerateNewId().ToString();

            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(14);
            testDate = testDate.AddMinutes(5);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(18);
            testDate2 = testDate2.AddMinutes(30);
            testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(9);
            testDate4 = DateTime.Now.Date;
            testDate4 = testDate4.AddHours(14);
            testDate4 = testDate4.AddMinutes(5);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac3.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                fac3.freeAppointments.Add(new DbAppointment(testDate4, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);
            _mongoCrud.SaveOrUpdate(fac3);






            /* POST START */
            locs = new List<string>();
            locs.Add("‏הירקון 2, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.PackagesCollection;
            company.Type = CompanyType.PostOffice;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(16);
            testDate = testDate.AddMinutes(55);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(18);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);







            locs = new List<string>();
            locs.Add("‏ז'בוטינסקי 12, תל אביב");
            locs.Add("‏הירקון 9, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.PackagesShipping;
            company.Type = CompanyType.PostOffice;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = locs[1];
            company2.SubType = CompanySubType.PackagesShipping;
            company2.Type = CompanyType.PostOffice;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();
            
            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(16);
            testDate = testDate.AddMinutes(55);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(9);
            testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(16);
            testDate3 = testDate3.AddMinutes(55);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);







            locs = new List<string>();
            locs.Add("‏פראג 1, תל אביב");
            locs.Add("‏התאנה 9, תל אביב");

            company = new Company();
            company.Id = ObjectId.GenerateNewId().ToString();
            company.Location = locs[0];
            company.SubType = CompanySubType.PaymentsAndLettersShipping;
            company.Type = CompanyType.PostOffice;
            company.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company);

            company2 = new Company();
            company2.Id = ObjectId.GenerateNewId().ToString();
            company2.Location = locs[1];
            company2.SubType = CompanySubType.PaymentsAndLettersShipping;
            company2.Type = CompanyType.PostOffice;
            company2.UrlForApi = "http://some.uri.com/";
            _mongoCrudCompany.SaveOrUpdate(company2);

            fac = new DbAppointmentCompany();
            fac.Company = company;
            fac.Id = ObjectId.GenerateNewId().ToString();

            fac2 = new DbAppointmentCompany();
            fac2.Company = company2;
            fac2.Id = ObjectId.GenerateNewId().ToString();

            testDate = DateTime.Now.Date;
            testDate = testDate.AddHours(13);
            testDate2 = DateTime.Now.Date;
            testDate2 = testDate2.AddHours(18);
            testDate2 = testDate2.AddMinutes(5);
            testDate3 = DateTime.Now.Date;
            testDate3 = testDate3.AddHours(16);
            testDate3 = testDate3.AddMinutes(55);

            endDate = testDate.AddMonths(1);

            times = new List<string>();
            while (testDate.ToString() != endDate.ToString())
            {
                fac.freeAppointments.Add(new DbAppointment(testDate, 30, "", ObjectId.GenerateNewId().ToString()));
                fac.freeAppointments.Add(new DbAppointment(testDate2, 30, "", ObjectId.GenerateNewId().ToString()));
                fac2.freeAppointments.Add(new DbAppointment(testDate3, 30, "", ObjectId.GenerateNewId().ToString()));
                testDate = testDate.AddDays(1);
            }

            _mongoCrud.SaveOrUpdate(fac);
            _mongoCrud.SaveOrUpdate(fac2);




            return null;
        }

        [HttpGet]
        public void AlterAppointments()
        {
            List<DbAppointmentCompany> dacs = _mongoCrud.GetAllEntities().ToList();

            foreach(DbAppointmentCompany dac in dacs)
            {
                List<DbAppointment> daRemoval = new List<DbAppointment>();
                foreach(DbAppointment da in dac.freeAppointments)
                {
                    if (da.StartTime.Hour < 21 && da.StartTime.Hour >= 6)
                    {
                        daRemoval.Add(da);
                    }
                }

                dac.freeAppointments.RemoveRange(0, dac.freeAppointments.Count);
                dac.freeAppointments.AddRange(daRemoval);

                var t = dac;
                _mongoCrud.SaveOrUpdate(dac);
            }
        }
    }
}
