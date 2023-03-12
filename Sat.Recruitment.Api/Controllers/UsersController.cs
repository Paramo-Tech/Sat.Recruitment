using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Global.Interfaces;
using Sat.Recruitment.Global.WebContracts;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;
        // Inject the logger
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<UserResult> CreateUser(User newUser)
        {
            try
            {
                // Retrive user list
                var userList = await _usersService.GetUsers();

                // Process user email and money
                var userProcessed = _usersService.ProcessUser(newUser);

                if (userList.Any(user => (user.Name == userProcessed.Name && user.Address == userProcessed.Address) ||
                                       user.Email == userProcessed.Email || user.Phone == userProcessed.Phone))
                {
                    _logger.LogError("The user is duplicated");

                    return new UserResult(false, "The user is duplicated");
                }

                _logger.LogInformation("The User Created");

                return new UserResult(true, "User Created");

            }
            catch (AggregateException e)
            {
                _logger.LogError(e.Message);
                return new UserResult(false, e.Message);
            }
        }
    }
}