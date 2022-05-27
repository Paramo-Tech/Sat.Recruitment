using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.ApiModels;
using Sat.Recruitment.Api.Services.Contracts;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IStoreServices _storeServices;


        private readonly List<User> _users = new List<User>();
        public UsersController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        [HttpGet]
        [Route("/ping")]
        public async Task<string> Test()
        {
            return await Task.Run(()=>"pong");
        }
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserRequest model)
        {
            var errors = "";

            ValidateErrors(model.name, model.email, model.address, model.phone, ref errors);

            if (errors != null && errors != "")
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            var newUser = new User
            {
                Name = model.name,
                Email = model.email,
                Address = model.address,
                Phone = model.phone,
                UserType = model.userType,
                Money = model.money
            };

            if (newUser.UserType == UserType.Normal)
            {
                if (model.money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif =model.money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
                if (model.money < 100)
                {
                    if (model.money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = model.money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == UserType.SuperUser)
            {
                if (model.money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = model.money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == UserType.Premium)
            {
                if (model.money > 100)
                {
                    var gif =model.money * 2;
                    newUser.Money = newUser.Money + gif;
                }
            }


            var reader = this._storeServices.ReadUsersFromFile();

            //Normalize email
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var currentUserType = (UserType)Enum.Parse(typeof(UserType), line.Split(',')[4].ToString());
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = currentUserType,
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();
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
                            throw new Exception("User is duplicated");
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
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}
