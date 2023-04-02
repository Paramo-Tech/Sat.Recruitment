using Application.Contracts;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly List<User> _users = new List<User>();
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser([FromQuery] UserCreationDto userCreation)
        {
            var result = await _service.Create(userCreation);

            return !result.IsSuccess ? Result.Failure(result.Errors) :
                    Result.Success(result.Message);
        }
    }

}
