using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Common.Exceptions;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Application.Users.Commands;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create([FromQuery] CreateUserCommand command)
        {
            try
            {
                await _mediator.Send(command);
                _logger.LogDebug("User Created");

                return Ok(new Result { IsSuccess = true, Errors = "User Created" });
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "Validation Errror.");

                return Ok(new Result { IsSuccess = false, Errors = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");

                return Ok(new Result { IsSuccess = false, Errors = ex.Message });
            }
        }
    }
}
