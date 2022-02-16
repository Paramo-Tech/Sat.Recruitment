using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.BusinessRules;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult<Result>> CreateUser(CreateUserRequest request)
        {
            try
            {
                var newUser = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Address = request.Address,
                    Phone = request.Phone,
                    UserType = request.UserType,
                    Money = request.Money
                };

                return _userService.Create(newUser);
            }
            catch (EntityAlreadyExistsException ex)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    ex.Message
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
