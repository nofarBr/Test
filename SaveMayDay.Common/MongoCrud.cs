using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SaveMyDate.Entities;

namespace SaveMayDay.Common
{
    public class MongoCrud<T> where T : class, IMongoEntity
    {
        private readonly MongoDbHandler<T> _mongoDbHandler;

        public MongoCrud()
        {
            _mongoDbHandler = new MongoDbHandler<T>();
        }

        public IQueryable<T> GetAllEntities()
        {
            return _mongoDbHandler.Collection.FindAll().AsQueryable();
        }

        public void SaveOrUpdate(T entityToSave)
        {             
            _mongoDbHandler.Collection.Save(entityToSave);
        }

        //public IQueryable<T> GetContact(string id)
        //{
        //    IMongoQuery query = Query.EQ("_id", id);
        //    return _mongoDbHandler.Collection.Find(query).FirstOrDefault();
        //}
}
}
