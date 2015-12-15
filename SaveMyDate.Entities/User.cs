using MongoDB.Bson;

namespace SaveMyDate.Entities
{
    public class User : IMongoEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
