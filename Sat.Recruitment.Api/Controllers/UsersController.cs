using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Factory;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Repository.Interfaces;
using Sat.Recruitment.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models.Validators;

namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly IUserService service;
        private readonly IValidator<UserDTO> validator;

        public UsersController(IUserService service, IValidator<UserDTO> validator)
        {
            this.service = service;
            this.validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDTO dto)
        {
            var result=validator.Validate(dto);
            if (result.IsValid)
            {

                service.CreateUser(dto);
                return Ok("User Created");
            }
            else return BadRequest(string.Join(",",result.Errors));
        }

    }

}
