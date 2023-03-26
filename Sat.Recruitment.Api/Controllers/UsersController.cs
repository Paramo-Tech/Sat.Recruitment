using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.BO;
using Sat.Recruitment.BLL;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var errors = ErrorValidations.BuildErrorMessage(name, email, address, phone);

            if (!string.IsNullOrEmpty(errors))
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            decimal.TryParse(money, out decimal moneyValue);

            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = moneyValue,
            };

            //User money and Bonus
            newUser.Money = UserValidations.GetUserTypeMoney(userType, money, moneyValue);

            //Normalize email
            newUser.Email = UserValidations.NormalizeEmail(newUser.Email);

            if (!Regex.IsMatch(email, pattern))
            {
                errors = ErrorValidations.BuildErrorMessage("email");

                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }

            using (var reader = ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    _users.Add(user);
                }
            }

            try
            {
                var isDuplicated = false;
                foreach (var user in _users)
                {
                    if (user.Email == newUser.Email
                        ||
                        user.Phone == newUser.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (user.Name == newUser.Name)
                    {
                        if (user.Address == newUser.Address)
                        {
                            isDuplicated = true;
                            throw new Exception(ErrorValidations.BuildErrorMessage(0));
                        }

                    }
                }

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine(ErrorValidations.BuildErrorMessage(0));

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = ErrorValidations.BuildErrorMessage(0)
                    };
                }
            }
            catch
            {
                Debug.WriteLine(ErrorValidations.BuildErrorMessage(0));
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ErrorValidations.BuildErrorMessage(0)
                };
            }
        }
    }
}
