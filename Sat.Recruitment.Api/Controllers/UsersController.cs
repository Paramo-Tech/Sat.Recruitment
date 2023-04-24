using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.Services.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IValidator<UserCreateRequest> createUserValidation;

        public UsersController(
            IUsersService usersService,
            IValidator<UserCreateRequest> createUserValidation)
        {
            this.usersService = usersService;
            this.createUserValidation = createUserValidation;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateRequest request)
        {
            var validationResult = this.createUserValidation.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            try
            {
                var response = await this.usersService.Create(request);

                if (response.Success)
                {
                    return Created("User Created", response.User);
                }

                return Conflict("The user already exists!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}