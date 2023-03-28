using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.DTOs;
using Sat.Recruitment.Application.Interfaces.Services;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Exceptions;
using System;
using System.Collections.Generic;

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

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            try
            {
                return Ok(this._userService.GetUsers());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("/create-user")]
        public ActionResult CreateUser(User user)
        {
            try
            {
                _userService.CreateNewUser(user);
                return StatusCode(201);
            }
            catch (DuplicatedUserException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
