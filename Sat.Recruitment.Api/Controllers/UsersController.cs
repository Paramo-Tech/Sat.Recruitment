using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {

        //private readonly List<User> _users = new List<User>();
        private UserService _service;
        public UsersController()
        {
            _service = new UserService();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (_service.Create(newUser))
            {
                return Ok(new Result
                {
                    IsSuccess = true,
                    Errors = "User Created"
                });
            }
            return Ok(new Result
            {
                IsSuccess = false,
                Errors = "The user is duplicated"
            });

        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

}
