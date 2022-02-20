using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IUserService userService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userService = userService;
        }


        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest request)
        {
            try
            {
                // Map the request parameters to the business entity
                User user = _mapper.Map<User>(request);

                // Persist the new entity
                user = await _userService.Create(user);

                _logger.LogInformation($"User created. Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Address: {user.Address}, Phone: {user.Phone}");

                // Map the new entity to response DTO
                CreateUserResponse response = _mapper.Map<CreateUserResponse>(user);

                return Created("", response);
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

        [HttpGet]
        public async Task<ActionResult<List<CreateUserResponse>>> ListUsers()
        {
            try
            {
                // TODO: Implement a request schema to support filtering

                // Get all the persisted Users
                List<User> users = await _userService.GetAll();

                // Map all the users to response DTO
                List<CreateUserResponse> response = _mapper.Map<List<CreateUserResponse>>(users);

                return Ok(response);
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
