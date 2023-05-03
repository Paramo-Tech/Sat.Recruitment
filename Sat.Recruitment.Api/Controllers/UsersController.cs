using Application.InterfacesApplication;
using Infraestructure.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IUserUseCase _userUseCase;

        public UsersController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpGet]
        [Route("/test")]
        public async Task<IActionResult> Index()
        {
            return Ok("This work ok");
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser(UserDto userRequest)
        {
            try
            {
                var create = await _userUseCase.CreateUser(_userUseCase.CreateUserDomain(userRequest.Name, userRequest.Email, userRequest.Address, userRequest.Phone, userRequest.UserType, userRequest.Money));
                if (create)
                {
                    var result = new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                    return Ok(result);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                //logger errors
                return BadRequest(ex.Message);

            }
        }


        [HttpGet]
        [Route("/get-user")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userUseCase.GetUser(id);
            return Ok(user);
        }

    }
}