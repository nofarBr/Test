using MongoDB.Bson;


namespace SaveMyDate.Entities
{
    public class Company : IMongoEntity
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public CompanyType Type { get; set; }
        public CompanySubType SubType { get; set; }
        public string UrlForApi { get; set; }

        public Company()
        {
        }
    }
}
