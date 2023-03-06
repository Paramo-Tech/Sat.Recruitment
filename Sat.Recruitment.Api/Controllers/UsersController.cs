using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Application.Users.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<int> Create(CreateUserCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        [Route("/get-users")]
        public async Task<IList<GetUserDto>> GetUsers()
        {
            return await mediator.Send(new GetUserQuery());
        }
    }    
}