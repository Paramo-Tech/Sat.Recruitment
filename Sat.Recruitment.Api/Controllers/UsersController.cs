using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
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
        public Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = ValidateErrors(name, email, address, phone);

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

            var reader = ReadUsersFromFile();

            newUser.Email = NormalizeEmail(newUser.Email);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
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
            reader.Close();
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

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });

            return email;
        }

        private string ValidateErrors(string name, string email, string address, string phone)
        {
            string errors = string.Empty;

            if (name == null)
                errors = "The name is required";
            if (email == null)
                errors += " The email is required.";
            if (address == null)
                errors += " The address is required.";
            if (phone == null)
                errors += " The phone is required.";

            return errors;
        }
    }
}
