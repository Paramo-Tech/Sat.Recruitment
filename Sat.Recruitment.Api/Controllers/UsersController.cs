using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Application.Users.Queries.GetUsers;

namespace Sat.Recruitment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("/create-user")]
    public async Task<IActionResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
    {
        try
        {
            var createUserCommand = new CreateUserCommand
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };
            var _ = await _mediator.Send(createUserCommand);

            return Ok(Result.Success("User Created"));
        }
        catch (FluentValidation.ValidationException validationException)
        {
            return Ok(Result.Error(validationException.ToErrorMessage()));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }
}
