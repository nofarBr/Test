using System.Collections.Generic;
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

        public BsonDocument[] GetDistinctCompanyTypes()
        {
            var match = new BsonDocument
                {
                    { "$match", new BsonDocument { } }
                };

            var group = new BsonDocument
                {
                    { "$group", new BsonDocument {
                                        {
                                            "_id", "$SubType"
                                        },
                                        {
                                            "type", new BsonDocument { { "$first", "$Type" } }
                                        }
                                }
                    }
                };
            // "_id", "$SubType", "type", new BsonDocument { "$first", "$Type" } }
            var pipeline = new AggregateArgs();
            pipeline.Pipeline = new[] { match, group };
            return _mongoDbHandler.Collection.Aggregate(pipeline).ToArray();
        }

        public T GetEntityById(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            return _mongoDbHandler.Collection.Find(query).FirstOrDefault();
        }

        public List<T> GetEntityByCompanySubType(string subType)
        {
            IMongoQuery query = Query.EQ("Company.SubType", subType);
            return _mongoDbHandler.Collection.Find(query).ToList();
        }
    }
}
