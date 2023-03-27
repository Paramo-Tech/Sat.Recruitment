using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("userType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }
        [BsonElement("money")]
        public decimal Money { get; set; }

         
    }
    public enum UserType
    {
        Normal,
        SuperUser,
        Premium
    }
}
