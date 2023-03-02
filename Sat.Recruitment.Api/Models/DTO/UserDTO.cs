using System;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Api.Models.DTO
{
    public class UserDTO
    {
        private string name;
        private string email;
        private string address;
        private string phone;
        private UserTypes userType;
        private decimal money;

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonPropertyName("email")]
        public string Email { get => email; set => email = value; }

        [JsonPropertyName("address")]
        public string Address { get => address; set => address = value; }

        [JsonPropertyName("phone")]
        public string Phone { get => phone; set => phone = value; }

        [JsonPropertyName("userType")]
        public UserTypes UserType { get => userType; set => userType = value; }

        [JsonPropertyName("money")]
        public decimal Money { get => money; set => money = value; }



    }
}
