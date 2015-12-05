using SaveMyDate.Entities;
using System.Web.Http;
using MongoDB.Driver;
using SaveMayDay.Common;
using System.Linq;
using SaveMyDate.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.OData;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SaveMyDay.WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly MongoDbHandler<Company> _MongoDbHandler;
        public CompanyController()
        {
            _MongoDbHandler = new MongoDbHandler<Company>();
        }

        [EnableQueryAttribute]
        public IQueryable<Company> GetCompanyByKindAndLocation(string Type, [FromUri]City city)
        {
            return _MongoDbHandler.Collection.FindAll().AsQueryable();
        }
    }
}
