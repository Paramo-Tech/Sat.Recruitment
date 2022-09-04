using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Infrastructure.Interfaces.Bussiness;
using Sat.Recruitment.Model;
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
    public class UsersController : ControllerBase
    {
        private IUserBussiness _userBussiness;

        public UsersController(IUserBussiness userBussiness)
        {
            _userBussiness = userBussiness;
            Console.WriteLine("is being initializaded");
        }

        [HttpPost]
        [Route("/create-user")]
        public Result CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            try
            {
                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Address = address,
                    Phone = phone,
                    UserType = userType,
                    OriginalMoney = decimal.Parse(money)
                };
                _userBussiness.CreateUser(newUser);

                return new Result
                {
                    Errors = "User Created",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new Result
                {
                    Errors = ex.Message,
                    IsSuccess=false
                };
            }
        }


    }
}
