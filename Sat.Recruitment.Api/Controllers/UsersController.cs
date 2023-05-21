using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Validators;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Services.Interfaces;
using System;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var newUser = new UserDTO
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            if (!UserValidator.Validate(newUser, out var errors))
            {
                return StatusCode(400,(new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                }));
            }

            try
            {
                await _userService.SaveUser(newUser);

                _logger.LogDebug("User Created. Name: " + newUser.Name);

                return Ok(new Result()
                {
                    IsSuccess = true                    
                });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Cannot create the user. ERROR: " + ex.Message);

                if (ex.Message == "The user is duplicated")
                {
                    return Conflict(new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user already exist"
                    }); 
                }
                else
                {
                    return StatusCode(500, new Result()
                    {
                        IsSuccess = false,
                        Errors = ex.Message
                    });
                }
            }
        }
    }
}
