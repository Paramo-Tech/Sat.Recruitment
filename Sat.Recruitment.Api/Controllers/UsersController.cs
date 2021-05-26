﻿using Domain;
using Microsoft.AspNetCore.Mvc;

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

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public Result CreateUser(User user)
        {
            var errors = "";
            var name = user.Name;
            var email = user.Email;
            var address = user.Address;
            var phone = user.Phone;
            var money = user.Money;
            var userType = user.UserType;

            ValidateErrors(name, email, address, phone, ref errors);

            if (errors != null && errors != "")
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }

            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };

            if (newUser.UserType == "Normal")
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
                if (money < 100)
                {
                    if (money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (money > 100)
                {
                    var gif = money * 2;
                    newUser.Money = newUser.Money + gif;
                }
            }


            var reader = ReadUsersFromFile();

            //Normalize email
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var userToAdd = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(userToAdd);
            }
            reader.Close();
            try
            {
                var isDuplicated = false;
                foreach (var userRead in _users)
                {
                    if (userRead.Email == newUser.Email
                        ||
                        userRead.Phone == newUser.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (userRead.Name == newUser.Name)
                    {
                        if (userRead.Address == newUser.Address)
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
        }

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
            {
                //Validate if Name is null
                errors = "The name is required";
            }

            if (email == null)
            {
                //Validate if Email is null
                errors = errors + " The email is required";
            }

            if (address == null)
            {
                //Validate if Address is null
                errors = errors + " The address is required";
            }

            if (phone == null)
            {
                //Validate if Phone is null
                errors = errors + " The phone is required";
            }
        }
    }
}
