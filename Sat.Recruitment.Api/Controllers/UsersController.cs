using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.utilities;
using System.Linq;
using Sat.Recruitment.Api.services;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        private readonly ILogger<UserService> _logger;
        public UsersController(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var error = new List<string>();
            try
            {
                IUserService userService = new UserService(_logger);

                var response = await userService.CreateUser(name, email, address, phone, userType, money);
                return response;
            }
            catch(ArgumentException ex)
            {
               
                return new Result()
                {
                    IsSuccess = false,
                    Errors = new List<string>(){ ex.Message }
                };
            }
            catch (Exception ex)
            {

                return new Result()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToString() }
                };
            }


        }

    }
}
