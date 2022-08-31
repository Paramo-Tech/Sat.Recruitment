using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IData _data;
        private readonly IUtil _util;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IData data, IUtil util, ILogger<UsersController> logger)
        {
            this._data = data;
            this._util = util;
            this._logger = logger;
        }

        [HttpPost]
        [Authorize]
        [Route("/create-user")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto user)
        {
            try
            {
                if (user.UserType == 1)
                    user.Money = _util.MoneyTypeNormal(user.Money);
                if(user.UserType == 3)
                    user.Money = _util.MoneyTypePremium(user.Money);
                if(user.UserType == 2)
                    user.Money = _util.MoneyTypeSuperUser(user.Money);

                user.Email = _util.NormaliceEmail(user.Email);

                if (_data.Exist(user))
                    return Conflict(user);
                else
                    await _data.Save(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }

            return Ok(user);
        }
    }
}
