using MongoDB.Bson;

namespace SaveMyDate.Entities
{
    public interface IMongoEntity
    {
         ObjectId Id { get; set; }
    }
}
