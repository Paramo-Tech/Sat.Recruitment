using Sat.Recruitment.Domain.Models.Enum;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Domain.Models.Users
{
    public class User
    {
        public User()
        {
            
        }

        public User(string name, string email, string address, string phone, string userType, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = System.Enum.Parse<EUserType>(userType);
            Money = money;
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        /*         
            The original code is not validating if it's null or empty and saves it anyway. I'll try to avoid a problem
            for the profit validation or in the SaveToDataBase process (I asume it cant't be null)
         */

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EUserType UserType { get; set; } = EUserType.NotSpecified; 
        public decimal Money { get; set; }
    }
}
