using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application;

namespace Sat.Recruitment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserCreator creator;
    public UsersController(UserCreator creator)
    {
        this.creator = creator;
    }

    [HttpPost]
    [Route("/users")]
    //public async Task<ActionResult<UserResponse>> CreateUser(string name, string email, string address, string phone, string userType, string money)
    public async Task<ActionResult<UserResponse>> CreateUser([FromBody] UserRequest request)
    {
        //var user = await creator.Execute(new UserRequest() { Address= address, Email=email, Money=money, Name=name, Phone=phone, UserType= userType});
        var user = await creator.Execute(request);
        return Created("/users", user);
    }
}

