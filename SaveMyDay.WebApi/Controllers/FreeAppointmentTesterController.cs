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
    public class FreeAppointmentTesterController : ApiController
    {
        private readonly MongoCrud<FreeAppointmentCompany> _mongoCrud;
        private readonly MongoCrud<Company> _mongoCrudCompany;
        public FreeAppointmentTesterController()
        {
            _mongoCrud = new MongoCrud<FreeAppointmentCompany>();
            _mongoCrudCompany = new MongoCrud<Company>();
        }

        [HttpPost]
        public Company PostCompany(JObject jsonParam)
        {
            Company company = _mongoCrudCompany.GetEntityById(jsonParam.GetValue("companyId").ToString());
            FreeAppointmentCompany fac = new FreeAppointmentCompany();
            fac.Company = company;

            //_mongoCrud.SaveOrUpdate(company);
            return company;
        }
    }
}
