using System;
using System.IO;

namespace Sat.Recruitment.Api.Services
{
    public partial class UserService
    {
        private const string NormalType = "Normal";        
        private const string SuperUserType = "SuperUser";        
        private const string PremiumType = "Premium";

        public User CreateUser(){
            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = SetMoney(userType, money)
            };
            return newUser;
        }
        private decimal SetMoney(string userType, decimal money){
            decimal moneyCalculated = 0.0M;
            switch (userType)
            {
                case NormalType:
                    if (moneyAmount > 100)
                        moneyCalculated += moneyAmount * 0.12M;
                    else if (moneyAmount > 10 && moneyAmount <= 100)
                        moneyCalculated += moneyAmount * 0.8M;
                    break;

                case SuperUserType:
                    if (moneyAmount > 100)
                        newUser.Money += moneyAmount * 0.20M;
                    break;

                case PremiumType:
                    if (moneyAmount > 100)
                        newUser.Money += moneyAmount * 2;
                    break;
                default:
                        moneyCalculated = money;
                        break;
            }                
            return moneyCalculated;
        }
        public bool UserDuplicated(User user,List<User> _users) => _users.Any(user =>
                    (user.Email == newUser.Email || user.Phone == newUser.Phone) ||
                    (user.Name == newUser.Name && user.Address == newUser.Address)
                );

    }
}
