using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            this._logger = logger;
            this._userService = userService;
        }


        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest request)
        {
            try
            {
                // Map the request parameters to the business entity
                var newUser = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Address = request.Address,
                    Phone = request.Phone,
                    UserType = request.UserType,
                    Money = request.Money
                };

                // Persist the new entity
                User createdUser = _userService.Create(newUser);

                _logger.LogInformation($"User created. Name: {createdUser.Name}, Email: {createdUser.Email}, Address: {createdUser.Address}, Phone: {createdUser.Phone}");

                // Map the new entity to response DTO
                CreateUserResponse createUserRespose = new CreateUserResponse()
                {
                    Name = createdUser.Name,
                    Email = createdUser.Email,
                    Address = createdUser.Address,
                    Phone = createdUser.Phone,
                    UserType = createdUser.UserType,
                    Money = createdUser.Money
                };

                return Created("", createUserRespose);
            }
            catch (EntityAlreadyExistsException ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    ex.Message
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
