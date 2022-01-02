using Sat.Recruitment.Business;
using Sat.Recruitment.Common;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.DAL.Interfaces;
using System;
using System.Collections.Generic;
using Sat.Recruitment.Business.Helpers;
using Sat.Recruitment.Business.Types;
using Sat.Recruitment.Business.Validations;
using Sat.Recruitment.Services.Interfaces;

namespace Services
{
    public class UserService:IService
    {
        IRepository<User, string> _userReporsitory;
        IParser<User,string> _userParser;
        IParser<UserType, string> _userTypeParser;
        public List<string> Errors { get; set; }

        public UserService(IRepository<User, string> userRepository, IParser<User,string> userParse, IParser<UserType,string> userTypeParsers)
        {
            _userReporsitory = userRepository;
            _userParser = userParse;
            _userTypeParser = userTypeParsers;
            Errors = new List<string>();
        }


        public List<User> GetUsers()
        {
            return _userReporsitory.GetAll().Result;
        }

        public string AddUser(User user)
        {
            string id;

            var validator = new UserValidator();
            var result = validator.Validate(user);

            if(!result.IsValid)
            {
                 result.Errors.ForEach(err => Errors.Add(err.ErrorMessage));
                return null;
            }

            if (!UserExists(user))
            {
                CalcUserGift(user);
                id = _userReporsitory.Save(user).Result;
            }
            else
            {
                throw new Exception(AppConstants.Messages.USER_DUPLICATED);
            }
            return id;
        }
        public string AddUser(string name, string email, string address, string phone, string userType, string money)
        {
            var newUser = new User
            {
                Name = name,
                Email = email,
                Money = MoneyHelper.ParseMoney(money),
                Address = address,
                Phone = phone,
                UserType = _userTypeParser.Parse(userType)
            };
            return AddUser(newUser);
        }

        public User GetUser(string ID)
        {
            return _userReporsitory.Get(ID).Result;
        }

        private static void CalcUserGift(User user)
        {
            var userGift = user.Money * MoneyHelper.UserPercentege(user);
            user.Money += userGift;
        }

        public bool UserExists(User user)
        {
            var users = GetUsers();
            var result = users.Exists
                (
                    u => u.UserType == user.UserType &&
                    u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase) &&
                    u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase) &&
                    u.Address.Equals(user.Address, StringComparison.OrdinalIgnoreCase) &&
                    u.Phone.Equals(user.Phone, StringComparison.OrdinalIgnoreCase)
                );
            return result;
        }

    }

}
