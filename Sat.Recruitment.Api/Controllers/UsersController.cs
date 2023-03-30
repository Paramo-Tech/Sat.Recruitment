using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Interfaces;

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
