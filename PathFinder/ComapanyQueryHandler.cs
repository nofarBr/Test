using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace PathFinder
{
    // no need for this class, will be deleted!!!!
    public class ComapanyQueryHandler
    {
        private readonly MongoDbHandler<Company> _mongoDbHandler;

        public ComapanyQueryHandler()
        {
            _mongoDbHandler = new MongoDbHandler<Company>();
        }

        public List<Company> GetCompaniesByTypeAndLocation(List<string[]> companyTypeAndSubtypeList, string city)
        {
            return null;
            //return
            //    _mongoDbHandler.Collection.AsQueryable<Company>()
            //        .Where(x => companyTypeAndSubtypeList && x.Location == city)
            //        .ToList();
        }
    }
}