using Application.Commands;
using Application.Dtos;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/v2/users")]
    [ApiController]
    public class UsersControllerV2 : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersControllerV2(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            UserDto result = await _mediator.Send(new GetUserByEmailQuery { Email = email});
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            UserDto result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { email = result.Email }, result);
        }

    }
}
