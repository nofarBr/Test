using SaveMyDate.Entities;
using System.Web.Http;
using SaveMayDay.Common;
using System.Linq;
using System.Web.OData;

namespace SaveMyDay.WebApi.Controllers
{
    public class CompanyController : ODataController
    {
        private readonly MongoDbHandler<Company> _MongoDbHandler;
        public CompanyController()
        {
            _MongoDbHandler = new MongoDbHandler<Company>();
        }

       
        [EnableQuery]
        public IQueryable<Company> GetCompanyByKindAndLocation(string Type, [FromUri]City city)
        {
            return _MongoDbHandler.Collection.FindAll().AsQueryable();
        }
    }
}
