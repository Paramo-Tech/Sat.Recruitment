using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repository;
using System;
using System.Linq;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ApplyGift(User user)
        {

            if (user.UserType == UserEnums.Normal.ToString())
            {
                if (user.Money > 10)
                    user.Money = IncreasePercentage(user.Money, 0.8);
                
            }
            else if (user.UserType == UserEnums.SuperUser.ToString())
            {
                if (user.Money > 100)
                    user.Money = IncreasePercentage(user.Money, 0.20);
            }
            else if (user.UserType == UserEnums.Premium.ToString())
            {
                if (user.Money > 100)
                    user.Money = IncreasePercentage(user.Money, 0.20);
            }
            else
                throw new Exception("Invalid userType");
                
        }

        private decimal IncreasePercentage(decimal money, double percentage)
        {
            var gif = money * Convert.ToDecimal(percentage);
            return money + gif;
        }

        public bool IsDuplicated(User user)
        {
            var users = _userRepository.GetUsers();

            if (users.Where(
                    x => x.Email == user.Email || 
                        x.Phone == user.Phone || 
                        (x.Name == user.Name && x.Address == user.Address)).Any())
                return true;
            
            return false;
        }
    }
}
