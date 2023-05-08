using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Command;
using Sat.Recruitment.Application.Dto;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Domain.Enum;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserData)
        {
            var result = await _mediator.Send(new CreateUserCommand()
            {
                Address = createUserData.Address,
                Email = createUserData.Email,
                Name = createUserData.Name,
                Money = createUserData.Money,
                Phone = createUserData.Phone,
                UserType = createUserData.UserType
            });

            return Ok(result);
        }

        [HttpPost]
        [Obsolete("Deprecated, please switch to the new POST \"users\" endpoint.")]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            // REMARKS: Left old endpoint to avoid breaking changes since the firm of the old endpoint is different from the new one.
            try 
            {
                Enum.TryParse(userType, out UserType type);
                var result = await _mediator.Send(new CreateUserCommand()
                {
                    Address = address,
                    Email = email,
                    Name = name,
                    Phone = phone,
                    Money = decimal.Parse(money),
                    UserType = type
                });

                return new Result
                {
                    IsSuccess = true
                };
            }
            
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    Errors = ex.ToString()
                };
            }            
        }
    }

}
