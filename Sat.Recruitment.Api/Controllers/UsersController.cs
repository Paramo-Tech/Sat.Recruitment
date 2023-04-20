using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newUser.UserType == "Normal")
            {
                if (newUser.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = newUser.Money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
                if (newUser.Money < 100)
                {
                    if (newUser.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = newUser.Money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (newUser.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = newUser.Money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (newUser.Money > 100)
                {
                    var gif = newUser.Money * 2;
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

                    return Ok(new Result
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    });
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return Ok(new Result
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    });
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
              
                return Ok(new Result
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                });
            }

            return Ok(new Result
            {
                IsSuccess = true,
                Errors = "User Created"
            });
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
            //Validate if Phone is null
            if (phone == null)
                errors = errors + " The phone is required";
        }
    }

}
