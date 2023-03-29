using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Xml.Linq;
using System.Security.Policy;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        private readonly List<User> _users = new List<User>();
        
        public UsersController(ILogger<UsersController> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User user)
        {
            _logger.LogInformation($"Add user");
            return await _usersService.Add(user);
            
            ////var newUser = new User
            ////{
            ////    Name = user.Name,
            ////    Email = user.Email,
            ////    Address = user.Address,
            ////    Phone = user.Phone,
            ////    UserType = user.UserType,
            ////    Money = user.Money
            ////};

            ////try
            ////{
            ////    var isDuplicated = false;
            ////    foreach (var xuser in _users)
            ////    {
            ////        if (xuser.Email == newUser.Email
            ////            ||
            ////            xuser.Phone == newUser.Phone)
            ////        {
            ////            isDuplicated = true;
            ////        }
            ////        else if (xuser.Name == newUser.Name)
            ////        {
            ////            if (xuser.Address == newUser.Address)
            ////            {
            ////                isDuplicated = true;
            ////                throw new Exception("User is duplicated");
            ////            }

            ////        }
            ////    }

            ////    if (!isDuplicated)
            ////    {
            ////        Debug.WriteLine("User Created");

            ////        return new Result()
            ////        {
            ////            IsSuccess = true,
            ////            Errors = "User Created"
            ////        };
            ////    }
            ////    else
            ////    {
            ////        Debug.WriteLine("The user is duplicated");

            ////        return new Result()
            ////        {
            ////            IsSuccess = false,
            ////            Errors = "The user is duplicated"
            ////        };
            ////    }
            ////}
            ////catch
            ////{
            ////    Debug.WriteLine("The user is duplicated");
            ////    return new Result()
            ////    {
            ////        IsSuccess = false,
            ////        Errors = "The user is duplicated"
            ////    };
            ////}
        }

        [HttpGet]
        [Route("/get-users")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation($"Get all users");
            return await _usersService.GetAll();
        }
    }
}
