using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        public UsersExtension usersExtension;
        public UsersController()
        {
            usersExtension = new UsersExtension();
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(UserRequestData requestData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .FirstOrDefault();
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }

            try
            {
                var newUser = new User
                {
                    name = requestData.name,
                    email = usersExtension.normalizeUserEmail(requestData.email.Trim()),
                    address = requestData.address,
                    phone = requestData.phone,
                    userType = requestData.userType,
                    money = usersExtension.calculateUserMoneyPercentaje(requestData.userType, decimal.Parse(requestData.money))
                };

                //Fill _users from the data
                getUserFromFile(ReadUsersFromFile());

                var isDuplicated = false;
                foreach (User user in _users)
                {
                    if (user.email.Trim().ToLower() == newUser.email.Trim().ToLower() || user.phone == newUser.phone)
                    {
                        isDuplicated = true;
                    }
                    else if (user.name.Trim().ToLower() == newUser.name.Trim().ToLower())
                    {
                        if (user.address.Trim().ToLower() == newUser.address.Trim().ToLower())
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Error when creating User: {ex.Message}");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = $"Error when creating User: {ex.Message}"
                };
            }

        }
        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
        private void getUserFromFile(StreamReader reader)
        {
            try
            {
                while (reader.Peek() >= 0)
                {
                    string[] line = reader.ReadLineAsync()?.Result?.Split(',');
                    if (line != null)
                    {
                        var user = new User
                        {
                            name = line[0].ToString(),
                            email = line[1].ToString(),
                            phone = line[2].ToString(),
                            address = line[3].ToString(),
                            userType = (UserType)Enum.Parse(typeof(UserType), line[4].ToString()),
                            money = decimal.Parse(line[5].ToString()),
                        };
                        _users.Add(user);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
