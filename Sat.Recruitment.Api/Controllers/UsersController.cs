using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Contracts.Results;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Services.Interfaces;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService usersService)
        {
            _logger = logger;
            _userService = usersService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User user)
        {
            _logger.LogInformation($"Add user");
            return await _userService.AddItemAsync(user);
        }
    }
}
