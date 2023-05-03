using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Requests.Commands;
using Sat.Recruitment.Api.Requests.Queries;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser(AddUserCommand addUsercommand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _mediator.Send(addUsercommand);

                return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString()); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {   
            var user = await _mediator.Send(new GetUserQuery { Id = id });

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
