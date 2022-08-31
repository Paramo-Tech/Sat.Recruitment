using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Utilities;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly IData _data;
        private readonly IUtil _util;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IData data, ILogger<LoginController> logger, IUtil util)
        {
            this._data = data;
            this._logger = logger;
            this._util = util;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<ApiDto> Authenticate(LoginDto login)
        {
            ApiDto apiDto = new ApiDto();
            try
            {
                login.Password = _util.Md5_hash(login.Password);
                apiDto = _data.ValidateUser(login);

                if (apiDto.Email == null)
                    return NotFound(apiDto);
                else
                    apiDto.Token = _util.TokenJWT(apiDto.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }

            return Ok(apiDto);

        }
    }
}
