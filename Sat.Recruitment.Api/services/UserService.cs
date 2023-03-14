using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Controllers;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Sat.Recruitment.Api.utilities;

namespace Sat.Recruitment.Api.services
{
    public interface IUserService
    {
        Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money);
    }
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>();
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = new List<string>();
            Utils.ValidateErrors(name, email, address, phone, ref errors);

            if (!errors.Any())
            {
                return new Result()
                {
                    IsSuccess = false,
                    // Errors = errors
                };
            }

            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };
            AUserFactory factory = new UserFactory(_logger);
            IUserCreator creator = factory.GetUserCreator(userType);
            var result = creator.CreateUser(name, email, address, phone, userType, money);
    

            var isDuplicated = await Utils.IsDuplicatedUserAsync(newUser);
            if (isDuplicated)
            {
                _logger.LogInformation("The user is duplicated");

                errors.Add("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    // Errors = errors
                };
            }

            _logger.LogInformation("User Created");

            errors.Add("User Created");
            return new Result()
            {
                IsSuccess = true,
                // Errors = errors
            };
        }


    }
}
