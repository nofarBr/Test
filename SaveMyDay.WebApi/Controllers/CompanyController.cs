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

namespace SaveMyDay.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyController : ApiController
    {
        private readonly MongoCrud<Company> _mongoCrud;
        public CompanyController()
        {
            _mongoCrud = new MongoCrud<Company>();
        }
       
        [HttpGet]
        public List<JObject> Get()
        {
            var allCompanies = _mongoCrud.GetDistinctCompanyTypes();
            List<JObject> companies = new List<JObject>();
            foreach (var company in allCompanies)
            {
                companies.Add(new JObject(
                              new JProperty("companyType", Enum.GetName(typeof(CompanyType), company.GetElement("type").Value.AsInt32)),
                              new JProperty("SubType", Enum.GetName(typeof(CompanySubType), company.GetElement("subtype").Value.AsInt32))));
                            //new JProperty("SubType", company.GetElement("_id").Value.AsString)));
            }

            return companies;
        }

        [HttpPost]
        public HttpResponseMessage PostCompany([FromBody]Company company)
        {
            _mongoCrud.SaveOrUpdate(company);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
