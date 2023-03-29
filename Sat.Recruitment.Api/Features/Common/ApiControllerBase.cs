using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Utils;
using System.Net;

namespace Sat.Recruitment.Api.Features.Common
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Created(string message) =>
            StatusCode((int)HttpStatusCode.Created, Envelope.Ok(message));

        protected IActionResult BadRequest(string message) =>
            StatusCode((int)HttpStatusCode.BadRequest, Envelope.Error(message));
    }
}
