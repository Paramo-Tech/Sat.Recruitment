using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Models;
using Sat.Recruitment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public partial class UsersController : ControllerBase
    {
        private readonly List<User> _users = new List<User>();
        private readonly UserService _userService;

        public UsersController()
        {
            _userService = new UserService();
            _users = _userService.GetUserListFromFile();
        }

        /// <summary>
        /// Creates a new User. it does not allow duplicates.
        /// </summary>
        [HttpPost]
        public Task<Result> CreateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_users.Any(u => (u.Email == user.Email || u.Phone == user.Phone)
                                    || (u.Name == user.Name && u.Address == user.Address)))
                    {
                        return Task.FromResult(new Result()
                        {
                            IsSuccess = false,
                            Errors = "The user is duplicated"
                        });
                    }
                    else
                    {
                        if(!user.UserType.Equals("Normal") && !user.UserType.Equals("SuperUser") && !user.UserType.Equals("Premium"))
                        {
                            return Task.FromResult(new Result()
                            {
                                IsSuccess = false,
                                Errors = "Invalid UserType. It must be Normal, SuperUser or Premium."
                            }); ;
                        }
                        _userService.CreateUser(user);

                        return Task.FromResult(new Result()
                        {
                            IsSuccess = true,
                            Errors = "User Created"
                        });
                    }
                }
                catch (Exception ex)
                {
                    return Task.FromResult(new Result()
                    {
                        IsSuccess = false,
                        Errors = "We were not able to create the user. " + ex.Message
                    }); ;
                }
            }
            else
            {
                return Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = "Please fill all the required fields."
                }); ;
            }           
        }
    }
}
