using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace CompanySimulator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MedicalClinicController : ApiController
    {
        private readonly MongoCrud<DbAppointmentCompany> _mongoCrud;
        public MedicalClinicController()
        {
            _mongoCrud = new MongoCrud<DbAppointmentCompany>();
        }

        [HttpGet]
        public List<DbAppointmentCompany> Get(string subType, string location)
        {
            return _mongoCrud.GetEntityByCompanySubType(subType, location);
        }


        [HttpPost]
        public bool Post(JObject appointment)
        {
            //schedual appointment with company
            return true;
        }
    }
}
