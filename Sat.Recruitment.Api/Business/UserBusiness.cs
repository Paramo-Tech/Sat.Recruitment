using Sat.Recruitment.Api.Models;
using System.Security.Policy;
using System;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Repositories;

namespace Sat.Recruitment.Api.Business
{
    public class UserBusiness: IUserBusiness
    {
        protected IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public async Task<User> CreateUser(User user)
        {
            user.Money += Math.Round(AddUserGif(user), 2);
            user.Email = NormalizeEmail(user.Email);
            user.UserType = user.UserType ?? UserType.Normal.ToString();
            return await _userRepository.Insert(user);
        }
        public static string NormalizeEmail(string email)
        {
            string[] splittedEmail = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            string domain = splittedEmail[1];
            string leftSide = splittedEmail[0].Replace(".", "");
            int atIndex = leftSide.IndexOf("+", StringComparison.Ordinal);
            leftSide = atIndex < 0 ? leftSide : leftSide.Remove(atIndex);
            return $"{leftSide}@{domain}";
        }
        public static double AddUserGif(User user)
        {
            switch (user.UserType)
            {
                case nameof(UserType.Normal):
                    if (user.Money > 10 && user.Money < 100)
                    {
                        return user.Money * 0.08;
                    }
                    if (user.Money > 100)
                    {
                        return user.Money * 0.12;
                    }
                    break;
                case nameof(UserType.SuperUser):
                    if (user.Money > 100)
                    {
                        return user.Money * 0.2;
                    }
                    break;
                case nameof(UserType.Premium):
                    if (user.Money > 100)
                    {
                        return user.Money * 2;
                    }
                    break;
            }
            return 0;
        }
    }
}
