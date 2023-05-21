using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Respositories;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> SaveUser(UserDTO newUser)
        {
            var user = new User
            {
                Address = newUser.Address,
                Email = newUser.Email,
                Money = newUser.Money,
                Name = newUser.Name,
                Phone = newUser.Phone,
                UserType = MapUserType(newUser.UserType)
            };

            var users = _userRepository.ReadUsers();

            NormalizEmail(user);
            
            if (UserExists(user, users))
            {
                throw new Exception("The user is duplicated");
            }

            GiftCalculator.SetMoney(user.Money, user);

            _userRepository.CreateUser(user);
            
            return newUser;
        }

        private bool UserExists(User user, List<User> users)
        {
            foreach (var u in users)
            {
                if (u.Email == user.Email || u.Phone == user.Phone || (u.Name == user.Name && u.Address == user.Address))
                {
                    return true;
                }
            }
            return false;
        }

        private UserType MapUserType(string userType)
        {
            switch (userType)
            {
                case "Normal":
                    return UserType.Normal;
                case "SuperUser":
                    return UserType.SuperUser;
                case "Premium":
                    return UserType.Premium;
                default:
                    throw new ArgumentException("Invalid user type.");
            }
        }

        private static void NormalizEmail(User newUser)
        {
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }


    }
}
