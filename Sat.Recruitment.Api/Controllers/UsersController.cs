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
        public UserResult CreateUser(User newUser)
        {
            try
            {
                var userList = _usersService.UpdateUserList(newUser);

                if (userList.Any(user => (user.Name == newUser.Name && user.Address == newUser.Address) ||
                                       user.Email == newUser.Email || user.Phone == newUser.Phone))
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

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}