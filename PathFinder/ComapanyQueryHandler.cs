using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using SaveMayDay.Common;
using SaveMyDate.Entities;

namespace PathFinder
{
    public class ComapanyQueryHandler
    {
        private readonly MongoDbHandler<Company> _mongoDbHandler;

        public ComapanyQueryHandler()
        {
            _mongoDbHandler = new MongoDbHandler<Company>();
        }

        public List<Company> GetCompaniesByTypeAndLocation(List<CompanyType> companyType, City city)
        {
            return
                _mongoDbHandler.Collection.AsQueryable<Company>()
                    .Where(x => x.Type.In(companyType) && x.Location.City.Decription == city.Decription)
                    .ToList();
        }
    }
}