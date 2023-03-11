using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Services;
using Sat.Recruitment.Global.Interfaces;
using Sat.Recruitment.Global.WebContracts;
using System.Data;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<UserResult> CreateUser(User newUser)
        {
            try
            {
                var userList = await _usersService.GetUserList();

                var userProcessed = _usersService.ProcessUser(newUser);

                if (userList.Any(user => (user.Name == userProcessed.Name && user.Address == userProcessed.Address) ||
                                       user.Email == userProcessed.Email || user.Phone == userProcessed.Phone))
                {
                    Debug.WriteLine("The user is duplicated");

                    return new UserResult(false, "The user is duplicated");
                }

                Debug.WriteLine("User Created");

                return new UserResult(true, "User Created");

            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.Message);
                return new UserResult(false, e.Message);
            }
        }
    }
}