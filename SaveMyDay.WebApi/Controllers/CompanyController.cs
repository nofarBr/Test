using SaveMyDate.Entities;
using System.Web.Http;
using SaveMayDay.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.OData;

namespace SaveMyDay.WebApi.Controllers
{
    public class CompanyController : ODataController
    {
        private readonly MongoCrud<Company> _mongoCrud;
        public CompanyController()
        {
            _mongoCrud = new MongoCrud<Company>();
        }
       
        [EnableQuery]
        public IQueryable<Company> Get()
        {
            return _mongoCrud.GetAllEntities();
        }

        [HttpPost]
        public HttpResponseMessage PostCompany([FromBody]Company company)
        {
            _mongoCrud.SaveOrUpdate(company);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
