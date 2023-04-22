using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Helpers;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _service;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _service = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("BadRequest");
                return BadRequest(ModelState);
            }

            if (_service.Create(newUser))
            {
                _logger.LogInformation("User Created");
                return Ok(new { message = "User Created" });
            }
            _logger.LogWarning("The user is duplicated");
            return Conflict("The user is duplicated");

        }

    }

}
