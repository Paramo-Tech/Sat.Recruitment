using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly List<User> _users = new List<User>();
        private const string userDuplicated = "The user is duplicated";
        private const string userCreated = "User Created";
        private const string errorUnexpected = "An error occurred on the server. Please check the information and try again.";
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            try
            {
                MyValidations validations = new MyValidations();
                var errors = validations.ValidateErrors(name, email, address, phone);
                if (!string.IsNullOrEmpty(errors))
                    ResponseMessageForCreateUser(false, errors);

                User user = _userService.CreateUser(name, email, address, phone,userType, money);
                UserData userData = new UserData();
                StreamReader reader = userData.ReadUsersFromFile();
                Normalizator normalizator = new Normalizator();

                normalizator.NormalizeEmail(user, _users, reader);
                bool isDuplicated = _userService.UserDuplicated(user, _users);
                if(isDuplicated) 
                    ResponseMessageForCreateUser(false, userDuplicated); 
                else
                    ResponseMessageForCreateUser(true, userCreated);
            }
            catch (ex)
            {
                ResponseMessageForCreateUser(false, errorUnexpected);
            }
        }
        private Result ResponseMessageForCreateUser(bool IsSuccess, string message){
            Debug.WriteLine(message);
            return new Result()
            {
                IsSuccess = true,
                Errors = message
            };
        }
    }
}
