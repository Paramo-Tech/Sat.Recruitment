using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Contract.Users;
using Sat.Recruitment.Domain.Models.Users;
using Sat.Recruitment.Domain.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ExecutionResult> CreateUserAsync(User user)
        {
            var result = await _userService.AddItemAsync(user);
            _logger.LogInformation("User created");
            return result;
        }

        [HttpPut]
        [Route("/update-user")]
        public async Task<ExecutionResult> UpdateUserAsync(User user)
        {
            var result = await _userService.UpdateItemAsync(user);
            _logger.LogInformation("User updated");
            return result;
        }

        [HttpDelete]
        [Route("/delete-user")]
        public async Task<ExecutionResult> DeleteUser(User user)
        {
            var result = await _userService.DeleteItemAsync(user);
            _logger.LogInformation("User deleted");
            return result;
        }

        [HttpGet]
        [Route("/get-users")]
        public async Task<List<User>> GetUsers()
        {
            return await _userService.GetItemsAsync();
        }
    }
}
