using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Domain.ExtensionMethods;
using Sat.Recruitment.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : Controller
    {

        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User newUser)
        {
            return await _userService.ProcessingNewUser(newUser);

        }

    }
}
