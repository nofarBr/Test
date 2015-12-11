using System.Linq;
using MongoDB.Bson;
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

        public IQueryable<T> GetEntity()
        {
            return _mongoDbHandler.Collection.FindAll().AsQueryable();
        }

        public void SaveOrUpdate(T entityToSave)
        {   
             entityToSave.Id = ObjectId.GenerateNewId();
            _mongoDbHandler.Collection.Save(entityToSave);
        }
    }
}
