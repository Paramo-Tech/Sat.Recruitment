using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Core.DomainEntities;
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
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }


        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserRequest request)
        {
            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = decimal.Parse(request.Money)
            };

            if (newUser.UserType == "Normal")
            {
                if (decimal.Parse(request.Money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = decimal.Parse(request.Money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }
                if (decimal.Parse(request.Money) < 100)
                {
                    if (decimal.Parse(request.Money) > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = decimal.Parse(request.Money) * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (decimal.Parse(request.Money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = decimal.Parse(request.Money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (decimal.Parse(request.Money) > 100)
                {
                    var gif = decimal.Parse(request.Money) * 2;
                    newUser.Money = newUser.Money + gif;
                }
            }


            var reader = ReadUsersFromFile();

            #region Normalize email

            /* After some quick research, I discovered that the use of the . and the + are
             * Gmail-specific implementations, and that there is no standard to support them,
             * so implementing the "Normalize Email" functionality to all email providers
             * could lead to the generation of invalid addresses.
             * 
             * An official source on Google Blog:
             *    https://gmail.googleblog.com/2008/03/2-hidden-ways-to-get-more-from-your.html
             *    
             * Then, the location of the dots matters for emails on Microsoft Outlook,
             * Yahoo Mail, and Apple iCloud, to mention some. Dots don’t matter for Facebook,
             * and they aren’t used at all for Twitter handles.
             */

            /* Takes an string and splits it on the @ character, if any of the partitions
             * result in an empty array, it removes them from the result.
             * 
             * Important fact here: if the supplied string is not a string of type
             * somestring@anotherstring the algorithm will fail at runtime without exception
             * handling.
             */
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            /* Finds the location of the first occurrence of the + sign, and returns it
             * in with respect to a zero-based array. If the sign is not found, then return -1.
             */
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            /* If the string does not contain a + sign: then it replaces occurrences of the
             * sign . with empty spaces.
             * 
             * If the string does contain a + sign: then it replaces occurrences of the sign .
             * with empty spaces, and remove everything to the right of the + sign (including
             * the + sign)
             * */
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            // Compose the email again with the two parts of the chain.
            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            #endregion // Normalize email

            #region Get users from storage

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

            #endregion // Get users from storage

            #region Check duplicated user

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

            #endregion // Check duplicated user

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }
    }
}
