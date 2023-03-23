using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Commands.CreateUser;
using Sat.Recruitment.Application.Queries.GetUser;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
		{
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand user)
        {
            var response = await _mediator.Send(user);

            return Created(new Uri($"{Request.Path}/{response.Id}", UriKind.Relative), response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] int userId)
        {
            var query = new GetUserQuery { Id = userId };

            var response = await _mediator.Send(query);

            return Ok(response);
        }
	}
}

