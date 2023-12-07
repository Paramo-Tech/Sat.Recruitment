using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private List<User> _users = new List<User>();
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = _userService.ValidateErrors(name, email, address, phone);

            if (!String.IsNullOrEmpty(errors))
                return Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                });

            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            if (newUser.UserType == "Normal")
            {
                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = decimal.Parse(money) * percentage;
                    newUser.Money += gif;
                }
                if (decimal.Parse(money) < 100 && decimal.Parse(money) > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = decimal.Parse(money) * percentage;
                    newUser.Money += gif;
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = decimal.Parse(money) * percentage;
                    newUser.Money += gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (decimal.Parse(money) > 100)
                {
                    var gif = decimal.Parse(money) * 2;
                    newUser.Money += gif;
                }
            }

            newUser.Email = _userService.NormalizeEmail(newUser.Email);

            _users = _userService.GetUserListFromFile();

            try
            {
                var isDuplicated = false;
                foreach (var user in _users)
                {
                    if (user.Email == newUser.Email || user.Phone == newUser.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (user.Name == newUser.Name && user.Address == newUser.Address)
                    {
                        isDuplicated = true;
                        throw new Exception("User is duplicated");
                    }
                }

                if (isDuplicated)
                {
                    Debug.WriteLine("The user is duplicated");

                    return Task.FromResult(new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    });
                }
                else
                {
                    Debug.WriteLine("User Created");

                    return Task.FromResult(new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    });
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                });
            }
        }
    }
}
