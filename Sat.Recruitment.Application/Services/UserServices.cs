using Sat.Recruitment.Application.Contracts.Application;
using Sat.Recruitment.Application.Contracts.Persistence;
using Sat.Recruitment.Application.Helpers;
using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Application.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public ResultUser CreateUser(User user)
        {
            List<User> listUsers = GetUsers();
            user.Money = MoneyCalculation(user);
            user.Email = Helper.NormalizeEmail(user);



            if (listUsers.Any(u => (u.Email.Equals(user.Email) || u.Phone.Equals(user.Phone)) ||
                                   (u.Name.Equals(user.Name) && u.Address.Equals(user.Address))))
            {
                return new ResultUser { IsSuccess = false, Message = "The user is duplicated" };

            }
            return new ResultUser { IsSuccess = true, Message = "User Created" };
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        private decimal MoneyCalculation(User newUser)
        {
            decimal moneyReturn = newUser.Money;
            switch (newUser.UserType)
            {
                case "Normal":
                    {
                        if (newUser.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            var gif = newUser.Money * percentage;
                            moneyReturn = newUser.Money + gif;
                        }
                        if (newUser.Money > 10 && newUser.Money < 100)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = newUser.Money * percentage;
                            moneyReturn = newUser.Money + gif;
                        }

                        break;
                    }
                case "SuperUser":
                    {
                        if (newUser.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.20);
                            var gif = newUser.Money * percentage;
                            moneyReturn = newUser.Money + gif;
                        }
                        break;
                    }
                case "Premium":
                    {
                        if (newUser.Money > 100)
                        {
                            var gif = newUser.Money * 2;
                            moneyReturn = newUser.Money + gif;
                        }
                        break;
                    }
            }
            return moneyReturn;
        }
    }
}
