using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Models;
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
    public class UsersController : ControllerBase
    {
        private readonly IReadUsersFromFile _readUsersFromFile;
        private readonly IUtil _util;

        public UsersController(IReadUsersFromFile readUsersFromFile, IUtil util)
        {
            this._readUsersFromFile = readUsersFromFile;
            this._util = util;
        }

        [HttpPost]
        [Route("/create-user")]
        public ActionResult<UserDto> CreateUser(UserDto user)
        {
            try
            {
                if (user.UserType == UserType.Type.Normal)
                    user.Money = _util.MoneyTypeNormal(user.Money);
                if(user.UserType == UserType.Type.Premium)
                    user.Money = _util.MoneyTypePremium(user.Money);
                if(user.UserType == UserType.Type.SuperUser)
                    user.Money = _util.MoneyTypeSuperUser(user.Money);

                user.Email = _util.NormaliceEmail(user.Email);

                if (_readUsersFromFile.Exist(user))
                {
                    return Conflict(user);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }

            return Ok(user);
        }
    }
}
