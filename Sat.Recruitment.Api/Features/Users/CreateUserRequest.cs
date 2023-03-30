using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Api.Features.Users
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [EnumDataType(typeof(UserType))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
