using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Services.Users.Commands;
using Sat.Recruitment.Services.Users.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(ulong id)
        {
            try
            {
                var result = _mapper.Map<UserDto>(await _mediator.Send(new GetUserQuery(id), CancellationToken.None));

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message, ex.StackTrace);
                throw new Exception(Messages.GenericError);
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _mapper.Map<List<UserDto>>(await _mediator.Send(new GetAllUsersQuery(), CancellationToken.None));

                if (result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message, ex.StackTrace);
                throw new Exception(Messages.GenericError);
            }
        }

        [HttpGet("/ActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActiveUsers()
        {
            try
            {
                var result = _mapper.Map<List<UserDto>>(await _mediator.Send(new GetAllActiveUsersQuery(), CancellationToken.None));

                if (result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message, ex.StackTrace);
                throw new Exception(Messages.GenericError);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditUser([FromBody] UserEditionForm userDto)
        {
            try
            {
                var user = _mapper.Map<UserDto>(await _mediator.Send(new EditUserCommand(userDto), CancellationToken.None));

                if (user == null)
                    return BadRequest(Messages.DuplicationError);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace, ex.InnerException);

                if (ex.Message.Contains(Messages.DuplicateKey) || ex.Message.Contains(Messages.UniqueError))
                    throw new Exception(Messages.DuplicationError);
                else
                    throw new Exception(Messages.GenericError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationForm userDto)
        {
            try
            {
                var user = _mapper.Map<UserDto>(await _mediator.Send(new CreateUserCommand(userDto), CancellationToken.None));
                return this.CreatedAtAction(nameof(this.Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace, ex.InnerException);

                if (ex.Message.Contains(Messages.DuplicateKey) || ex.InnerException.Message.Contains(Messages.UniqueError))
                    throw new Exception(Messages.DuplicationError);
                else
                    throw new Exception(Messages.GenericError);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(ulong id)
        {
            try
            {
                var user = _mapper.Map<UserDto>(await _mediator.Send(new DeleteUserCommand(id), CancellationToken.None));

                if (user == null)
                    return BadRequest(Messages.DeleteError);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace, ex.InnerException);
                throw new Exception(Messages.GenericError);
            }
        }
    }

}
