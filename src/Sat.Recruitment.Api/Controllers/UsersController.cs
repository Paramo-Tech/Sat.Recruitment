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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
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
        public async Task<ActionResult<UpdateResponse>> Update(
            [Required]
            [FromRoute]
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
