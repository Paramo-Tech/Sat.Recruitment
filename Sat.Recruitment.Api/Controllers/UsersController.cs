using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Core.DataTransferObjects;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;
using Sat.Recruitment.Kernel.Features.Users.CreateUserCommand;
using Sat.Recruitment.Kernel.Features.Users.GetUserQueryByIdCommand;
using Sat.Recruitment.Kernel.Features.Users.GetUsersQueryCommand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserRequest> _validator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator to command handlers</param>
        /// <param name="validator">Validator of request command</param>
        public UsersController(IMediator mediator, IValidator<CreateUserRequest> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public async Task<ActionResult<IResponseResult<List<IUser>>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQueryRequest());

            return new ResponseResultOk<List<IUser>>( users, $"GET-200");
        }

        /// <summary>
        /// Returns user by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User information</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IResponseResult<IUser>>> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQueryRequest { Id = id });

            return new ResponseResultOk<IUser>(user, $"GET-200");
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResponseResult<string>> CreateUser(CreateUserRequest command)
        {
            var result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                throw new ValidationException(result.ToString());
            }
            var id = await _mediator.Send(command);
            return new ResponseResultOk<string>(id.ToString(), $"POST-200");
        }
    }
}
