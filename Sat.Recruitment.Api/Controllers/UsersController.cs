using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Interfaces;
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
        private readonly IUserService _userService;
        private readonly List<User> _users = new List<User>();
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/createUser")]
        public Result CreateUser(User user)
        {
            return _userService.createUser(user);
        }

        [HttpPost]
        [Route("/CreateUserAsync")]
        public async Task<Result> CreateUserAsync(User user)
        {
            return await _userService.CreateUserAsync(user);
        }

        [HttpGet]
        [Route("/GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet]
        [Route("/GetAllUsersAsync")]
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

    }
}
