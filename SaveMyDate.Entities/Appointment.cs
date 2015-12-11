using System;
using MongoDB.Bson;

namespace SaveMyDate.Entities
{
    // which custumer saved
       
    public class Appointment : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public Company Company { get; set; }
        public DateTime Time { get; set; }
        public DateTime LastModified { get; set; }
        public string Remark { get; set; }

        public Appointment()
        {
            
     
        }
    }
}
