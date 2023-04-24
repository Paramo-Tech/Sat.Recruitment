using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Business.User.CreateUser;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<CreateUserResult> CreateUser(CreateUserCommand createUserCommand)
        {
            return await mediator.Send(createUserCommand);
        }
    }

}
