using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Logic.Business;
using Sat.Recruitment.Api.Logic.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private List<UserRequest> _users = new List<UserRequest>();
        private readonly ILogger<UsersController> _logger;
        private readonly IUsers _usersObj;

        public UsersController(IUsers users, ILogger<UsersController> logger)
        {
            _usersObj = users;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<UserResponse> CreateUser(UserRequest newUser)
        {
            UserResponse result = null;

            try
            {
                var requiredFields = _usersObj.RequiredFieldsValidation(newUser);
                if (requiredFields.Any())
                {
                    throw new Exception(string.Join(' ', requiredFields));
                }

                newUser.Money = _usersObj.CalculateMoneyByUserType(newUser.UserType, newUser.Money);
                newUser.Email = _usersObj.NormalizeEmail(newUser.Email);

                _users = await _usersObj.ReadUsersFromFile();

                if (_users.Any(x => (x.Name == newUser.Name && x.Address == newUser.Address) ||
                (x.Email == newUser.Email || x.Phone == newUser.Phone)))
                {
                    throw new Exception("The user is duplicated");
                }

                _logger.LogDebug("User Created");

                result = new UserResponse
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            catch (Exception ex) 
            {
                _logger.LogDebug(ex.Message);

                result = new UserResponse
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }

            return result;
        }
    }
}
