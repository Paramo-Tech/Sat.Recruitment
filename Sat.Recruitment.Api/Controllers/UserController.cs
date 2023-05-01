using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Model.Shared;
using Sat.Recruitment.Model.User;
using Sat.Recruitment.Povider.IProvider;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProvider _userProvider;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserProvider userProvider, ILogger<UserController> logger)
        {
            _userProvider = userProvider;
            _logger = logger;
        }



        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ResponseModel),200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            ResponseModel response;
            try
            {
                response = await _userProvider.CreateUser(user);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception: " + e);
                response = new ResponseModel()
                {
                    Error = new ErrorModel
                    {
                        ErrorId = 001,
                        ErrorMessage = e.ToString()
                    },
                    IsSuccess = false
                };
                return BadRequest(response);
            }
        }

    }
}
