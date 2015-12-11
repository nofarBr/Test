using MongoDB.Bson;

namespace SaveMyDate.Entities
{
    public class Company : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public Location Location { get; set; }
        public CompanyType Type { get; set; }
        public string UrlForApi { get; set; }

        public Company()
        {
        }
    }
}
