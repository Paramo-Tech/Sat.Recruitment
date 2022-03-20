using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Services.Authentication.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IMediator mediator, IConfiguration configuration)
        {
            _logger = logger;   
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginForm loginForm)
        {
            try
            {
                var authenticatedUser = await _mediator.Send(new LoginCommand(loginForm.Email, loginForm.Password, (string)_configuration.GetValue(typeof(string), "Key")), CancellationToken.None);

                if (authenticatedUser == null)
                    return Unauthorized(Messages.WrongUserPassowrd);

                return Ok(authenticatedUser);
            }
            catch(Exception e)
            {
                _logger.LogError(e.InnerException, e.Message, e.StackTrace);
                throw new Exception(Messages.GenericError);
            }
        }
    }
}
