using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Users.Application.Commands.Create;
using Users.Domain;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IMediator mediator;

        public UsersController(
            ILogger<UsersController> logger,
            IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateUserDto createUserDto)
        {
            this.logger.LogDebug("UsersController - post", createUserDto);

            await this.mediator.Send(new CreateUserCommand
            {
                Name = createUserDto.Name,
                Email = new(createUserDto.Email),
                Address = createUserDto.Address,
                Phone = new(createUserDto.Phone),
                UserType = UserType.FromValue(createUserDto.UserType),
                Money = createUserDto.Money
            });

            return Ok();
        }
    }
}
