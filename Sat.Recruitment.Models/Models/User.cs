using Sat.Recruitment.Models.Enums;
using Sat.Recruitment.Models.ViewModels;
using System;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Models.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("userType")]
        public UserType UserType { get; set; }

        [JsonPropertyName("money")]
        public decimal Money { get; set; }

        public User()
        {

        }

        public User(UserModel userInput)
        {
            Id = Guid.NewGuid();
            Name = userInput.Name.Trim();
            Email = userInput.Email.Trim().ToLower();
            Address = userInput.Address.Trim();
            Phone = userInput.Phone.Trim();
            UserType = userInput.UserType;
            Money = GetMoneyWithGift(userInput.Money, userInput.UserType);
        }

        private decimal GetMoneyWithGift(decimal money, UserType userType)
        {
            var moneyResult = money;
            switch (userType)
            {
                case UserType.Normal:
                    if (money >= 100)
                        moneyResult = money * Convert.ToDecimal(1.12);

                    if (money < 100 && money > 10)
                        moneyResult = money * Convert.ToDecimal(1.8);
                    break;
                case UserType.Premium:
                    if (money > 100)
                        moneyResult = money * 2;
                    break;
                case UserType.SuperUser:
                    if (money > 100)
                        moneyResult = money * Convert.ToDecimal(1.20);
                    break;
                default:
                    break;
            }

            return moneyResult;
        }
    }
}