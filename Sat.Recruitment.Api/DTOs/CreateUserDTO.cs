using Sat.Recruitment.Api.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Api.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get ; set; }
        public decimal Money { get; set; }
    }
}
