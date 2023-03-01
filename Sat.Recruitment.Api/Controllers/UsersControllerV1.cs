using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Application;
using Sat.Recruitment.Application.Core;
using Sat.Recruitment.Application.Dtos;

namespace Sat.Recruitment.Api.Controllers
{
    public  class UsersControllerV1 : BaseApiController
    {

        private readonly List<User> _users = new List<User>();

        [HttpPost]
        [Route("/create-userv1")]
        public async Task<IActionResult> CreateUserMethod(CreateUserDto userDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return HandleResult(Result<object>.Failure("error"));
            //}
            var userService = new UserService();
            return HandleResult(await userService.CreateUser(userDto));

        }

    }
}
