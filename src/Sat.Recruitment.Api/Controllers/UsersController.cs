using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTOs.Users;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [SwaggerOperation(
            Summary = "Registers a new User",
            Description = "Registers a new User",
            OperationId = "Users.Create")]
        [SwaggerResponse(201, "Successfully created.")]
        [SwaggerResponse(409, "The User data entered conflicts with User data already registered.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<ActionResult<CreateResponse>> Create(CreateRequest request)
        {
            try
            {
                // Map the request parameters to the business entity
                User user = _mapper.Map<User>(request);

                // Persist the new entity
                user = await _userService.Create(user);

                // Write in log
                _logger.LogInformation($"User created. Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Address: {user.Address}, Phone: {user.Phone}");

                // Map the new entity to response DTO
                CreateResponse response = _mapper.Map<CreateResponse>(user);

                return this.CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
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
        [SwaggerOperation(
            Summary = "Lists all the registered Users.",
            Description = "Lists all the registered Users.",
            OperationId = "Users.List")]
        [SwaggerResponse(200, "Users successfully found.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<ActionResult<List<CreateResponse>>> List()
        {
            try
            {
                // TODO: Implement a request schema to support filtering

                // Get all the persisted Users
                List<User> users = await _userService.GetAll();

                // Map all the users to response DTO
                List<CreateResponse> response = _mapper.Map<List<CreateResponse>>(users);

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


        [HttpGet("{id:Guid}", Name = "GetById")]
        [SwaggerOperation(
            Summary = "Gets a User by its id",
            Description = "Gets a User by its id",
            OperationId = "Users.GetById")]
        [SwaggerResponse(200, "User successfully found.")]
        [SwaggerResponse(404, "Requested User not found.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<ActionResult<GetByIdResponse>> GetById(
            [FromRoute]
            [Required]
            [SwaggerParameter(Description = "Id of the User to be found", Required = true)]
            Guid id)
        {
            try
            {
                // Get persisted User
                User user = await _userService.GetById(id);

                // Map the new entity to response DTO
                GetByIdResponse response = _mapper.Map<GetByIdResponse>(user);

                return this.Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(
                    ex.Message
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                    );
            }
        }

        [HttpDelete("{id:Guid}")]
        [SwaggerOperation(
            Summary = "Deletes an existing User entity by its Id",
            Description = "Deletes an existing User entity by its Id",
            OperationId = "Users.Delete")]
        [SwaggerResponse(204, "User successfully deleted.")]
        [SwaggerResponse(404, "Requested User not found.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<IActionResult> Delete(
            [FromRoute]
            [Required]
            [SwaggerParameter(Description = "Id of the User to be deleted", Required = true)]
            Guid id)
        {
            try
            {
                // Delete persisted User
                await _userService.Delete(id);

                // Write in log
                _logger.LogInformation($"User deleted. Id: {id}");

                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(
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

        [HttpPut("{id:Guid}")]
        [SwaggerOperation(
            Summary = "Updates an existing User entity by its Id, and the provided payload.",
            Description = "Updates an existing User entity by its Id, and the provided payload.",
            OperationId = "Users.Update")]
        [SwaggerResponse(200, "User successfully updated")]
        [SwaggerResponse(404, "Requested User not found.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<ActionResult<UpdateResponse>> Update(
            [FromRoute]
            [Required]
            [SwaggerParameter(Description = "Id of the User to be Updated", Required = true)]
            Guid id,

            [FromBody] UpdateRequest request)
        {
            try
            {
                // Map the request parameters to the business entity
                User user = _mapper.Map<User>(request);
                user.Id = id;

                // Update persisted User
                user = await _userService.Update(user);

                // Write in log
                _logger.LogInformation($"User updated. Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Address: {user.Address}, Phone: {user.Phone}");

                // Map the new entity to response DTO
                UpdateResponse response = _mapper.Map<UpdateResponse>(user);

                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(
                    ex.Message
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                    );
            }
        }
    }
}
