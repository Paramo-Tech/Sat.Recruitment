using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Infrastructure.MongoDb
{
	public class UserDocument
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }

    //public class People
    //{
    //    [BsonElement("name")]
    //    public string Name { get; set; }

    //    [BsonElement("age")]
    //    public int Age { get; set; }

    //    [BsonElement("phonenumbers")]
    //    public List<string> PhoneNumbers { get; set; }
    //}
}

