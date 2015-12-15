using MongoDB.Bson;

namespace SaveMyDate.Entities
{
    public interface IMongoEntity
    {
         string Id { get; set; }
    }
}
