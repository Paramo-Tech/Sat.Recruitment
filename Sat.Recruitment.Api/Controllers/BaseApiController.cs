using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Core;
using Microsoft.Extensions.DependencyInjection;


namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// a base controller to add all the generic stuffs 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}
