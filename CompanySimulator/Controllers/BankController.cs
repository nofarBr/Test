using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace CompanySimulator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankController : ApiController
    {
        private readonly MongoCrud<FreeAppointmentCompany> _mongoCrud;
        public BankController()
        {
            _mongoCrud = new MongoCrud<FreeAppointmentCompany>();
        }

        [HttpGet]
        public List<FreeAppointmentCompany> Get(string subType, string location)
        {
            return _mongoCrud.GetEntityByCompanySubType(subType, location);
        }
    }
}
