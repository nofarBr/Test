using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace CompanySimulator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankController : ApiController
    {
        private readonly MongoCrud<DbAppointmentCompany> _mongoCrud;
        public BankController()
        {
            _mongoCrud = new MongoCrud<DbAppointmentCompany>();
        }

        [HttpGet]
        public List<DbAppointmentCompany> Get(string subType, string location)
        {
            return _mongoCrud.GetEntityByCompanySubType(subType, location);
        }

        [HttpPost]
        public bool Post(int appointmentId)
        {
            //schedual appointment with company
            //delete appointment after schedual
            //_mongoCrud.Delete(appointmentId);
            return true;
        }
    }
}
