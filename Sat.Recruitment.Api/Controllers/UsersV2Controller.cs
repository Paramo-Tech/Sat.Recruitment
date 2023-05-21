using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Validators;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/v2/users")]
    public partial class UsersV2Controller : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersV2Controller> _logger;

        public UsersV2Controller(IUserService userService, ILogger<UsersV2Controller> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateUserV2([FromBody] UserDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Result()
                {
                    IsSuccess = false,
                    Errors = GetModelStateErrors()
                });
            }

            var newUser = new UserDTO
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = request.Money
            };

            if (!UserValidator.Validate(newUser, out var errors))
            {
                return BadRequest(new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                });
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
                        Errors = "The user already exists"
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Result()
                    {
                        IsSuccess = false,
                        Errors = ex.Message
                    });
                }
            }
        }

        [HttpGet("{email}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetUser(string email)
        {
            var user = await _userService.GetUser(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private string GetModelStateErrors()
        {
            var errors = new List<string>();

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return string.Join(" ", errors);
        }
    }
}
