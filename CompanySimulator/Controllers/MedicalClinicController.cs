using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace CompanySimulator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MedicalClinicController : ApiController
    {
        private readonly MongoCrud<FreeAppointmentCompany> _mongoCrud;
        public MedicalClinicController()
        {
            _mongoCrud = new MongoCrud<FreeAppointmentCompany>();
        }

        [HttpGet]
        public List<FreeAppointmentCompany> Get(string subType)
        {
            return _mongoCrud.GetEntityByCompanySubType(subType);
        }
    }
}
