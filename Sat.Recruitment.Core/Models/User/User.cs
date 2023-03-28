using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sat.Recruitment.Core.Models.User
{
    [Serializable]
    public class User : IUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string? Name { get; set; }

        [BsonElement("email")]
        [BsonRequired]
        public string? Email { get; set; }

        [BsonElement("address")]
        [BsonRequired]
        public string? Address { get; set; }

        [BsonElement("phone")]
        [BsonRequired]
        public string? Phone { get; set; }

        [BsonElement("userType")]
        public string? UserType { get; set; }

        [BsonElement("money")]
        public decimal Money { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }
    }
}