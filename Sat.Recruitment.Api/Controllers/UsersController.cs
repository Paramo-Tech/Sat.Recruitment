using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) { 
            _userService = userService;
        }

        private readonly List<User> _users = new List<User>();
        
        
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            try
            {
                _userService.ApplyGift(user);
                var isDuplicated =_userService.IsDuplicated(user);

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");
                    return Ok("User Created");
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");
                    return BadRequest("The user is duplicated");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
    
}
