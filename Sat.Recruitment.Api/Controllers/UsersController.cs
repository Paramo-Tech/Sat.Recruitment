using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Business.Contracts;
using Sat.Recruitment.Business.Models;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="reservationService">The integration service.</param>
        /// <exception cref="ArgumentNullException">service</exception>
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user">The user info <see cref="UserModel" />.</param>
        /// <response code="200">User created</response>
        [ProducesResponseType(200)]
        [HttpPost]
        [Route("/create-user")]
        public ResultModel CreateUser(UserModel user)
        {
            var result =_userService.CreateUser(user);
            return result;
        }
    }
}
