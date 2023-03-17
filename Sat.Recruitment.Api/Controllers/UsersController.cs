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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserService> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UsersController(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="address">The address.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="money">The money.</param>
        /// <returns></returns>
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
                _logger.LogInformation(ex.Message);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = new List<string>(){ ex.Message }
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToString() }
                };
            }


        }

    }
}
