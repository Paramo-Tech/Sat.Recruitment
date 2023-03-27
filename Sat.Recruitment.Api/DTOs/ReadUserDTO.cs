using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Sat.Recruitment.Api.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Api.DTOs 
{ 

    [ExcludeFromCodeCoverage]
    public class ReadUserDTO
    {
        [Required]
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
