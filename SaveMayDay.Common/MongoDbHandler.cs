using MongoDB.Driver;
using MongoDB.Bson;

namespace SaveMayDay.Common
{
    public class MongoDbHandler<T> where T : class
    {
        public MongoCollection<T> Collection { get; private set; }
        public MongoDbHandler()
        {
           
            MongoClient client = new MongoClient(@"mongodb://localhost:27017/SaveMayDay");
            var server = client.GetServer();
            var database = server.GetDatabase("SaveMayDay");

            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());

        }
    }
}
