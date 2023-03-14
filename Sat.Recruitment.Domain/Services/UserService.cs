using Mapster;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.IRepositories;

namespace Sat.Recruitment.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Insert(UserDto userDto)
        {
            //Here we can use adapt or automapper.

            User user = userDto.Adapt<User>();

            user.Money = AdaptMoneyToUserType(userDto.Money, userDto.UserType);
            user.Email = NormalizeEmail(userDto.Email);

            var newUser = await _userRepository.Insert(user);

            UserDto userResponse = newUser.Adapt<UserDto>();

            return userResponse;
        }
        
        public async Task<string> GetUserTypeByEmail(string email)
        {
            var userType = await _userRepository.GetUserTypeByEmail(email);

            return userType;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var isUserValid = await _userRepository.ValidateCredentials(email, password);

            return isUserValid;
        }

        private static string NormalizeEmail(string originalEmail)
        {
            var aux = originalEmail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            var emailNormalized = string.Join("@", new string[] { aux[0], aux[1] });
            return emailNormalized;
        }

        private static decimal AdaptMoneyToUserType(string money, string userType)
        { 
            decimal moneyAdapted = decimal.Parse(money);

            if (userType == UserTypeEnum.Normal.ToString())
            {
                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = decimal.Parse(money) * percentage;
                    moneyAdapted = moneyAdapted + gif;
                }
                if (decimal.Parse(money) < 100)
                {
                    if (decimal.Parse(money) > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = decimal.Parse(money) * percentage;
                        moneyAdapted = moneyAdapted + gif;
                    }
                }
            }
            if (userType == UserTypeEnum.SuperUser.ToString())
            {
                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = decimal.Parse(money) * percentage;
                    moneyAdapted = moneyAdapted + gif;
                }
            }
            if (userType == UserTypeEnum.Premium.ToString())
            {
                if (decimal.Parse(money) > 100)
                {
                    var gif = decimal.Parse(money) * 2;
                    moneyAdapted = moneyAdapted + gif;
                }
            }

            return moneyAdapted;
        }
    }
}
