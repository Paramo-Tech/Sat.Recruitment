using DAL;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Business;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Types;
using Sat.Recruitment.Common;
using Sat.Recruitment.DAL.Interfaces;
using Services;
using System;
using System.Collections.Generic;
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
        private readonly UserService _userService;
        private readonly List<User> _users = new List<User>();
        public UsersController(IRepository<User, string> userRepository, IParser<User,string> userParse, IParser<UserType, string> userTypeParse)
        {
            _userService =  new UserService(userRepository, userParse,userTypeParse);
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            string errors = string.Empty;
            try
            {
                _userService.AddUser(name, email, address, phone, userType, money);
            }
            catch(Exception ex)
            {
                errors = ex.Message;
            }

            if (errors != null && errors != "")
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };


            return new Result()
            {
                IsSuccess = true,
                Errors = AppConstants.Messages.USER_CREATED
            };
        }       
    }

}
