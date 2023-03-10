using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sat.Recruitment.Core
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ApiBaseController : ControllerBase
    {
       
    }
}
