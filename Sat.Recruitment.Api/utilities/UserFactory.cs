using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.utilities
{
    public interface IUserCreator
    {
        Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money);
    }
    public abstract class AUserFactory
    {
        public abstract IUserCreator GetUserCreator(string userType);

    }
    public class UserFactory : AUserFactory
    {
        private static ILogger<UserService> _logger;

        public UserFactory(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        public override IUserCreator GetUserCreator(string userType)
        {

            switch (userType)
            {
                case "Normal":
                    return new NormalUserCreator();
                case "SuperUser":
                    return new SuperUserCreator();
                case "Premium":
                    return new PremiumUserCreator();
                default:
                    throw new ArgumentException($"Invalid user type: {userType}");
            }
        }

        public class NormalUserCreator : IUserCreator
        {

            public Task<Result> CreateUser(string name, string email, string address, string phone, string userType,
                string money)
            {
                var newUser = CreateUserObject(name, email, address, phone, userType, money);

                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = decimal.Parse(money) * percentage;
                    newUser.Money += gif;
                }
                else if (decimal.Parse(money) > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = decimal.Parse(money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }

                return ValidateAndCreateUser(newUser);
            }
        }

        public class SuperUserCreator : IUserCreator
        {
            public Task<Result> CreateUser(string name, string email, string address, string phone, string userType,
                string money)
            {
                var newUser = CreateUserObject(name, email, address, phone, userType, money);

                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = decimal.Parse(money) * percentage;
                    newUser.Money += gif;
                }

                return ValidateAndCreateUser(newUser);
            }
        }

        public class PremiumUserCreator : IUserCreator
        {
            public Task<Result> CreateUser(string name, string email, string address, string phone, string userType,
                string money)
            {
                var newUser = CreateUserObject(name, email, address, phone, userType, money);

                if (decimal.Parse(money) > 100)
                {
                    var gif = decimal.Parse(money) * 2;
                    newUser.Money += gif;
                }

                return ValidateAndCreateUser(newUser);
            }
        }
        private static async Task<Result> ValidateAndCreateUser(User user)
        {
            // Validation code goes here
            var isDuplicated = await Utils.IsDuplicatedUserAsync(user);
            if (isDuplicated)
            {
                _logger.LogInformation("The user is duplicated");

                var error = new List<string> { "The user is duplicated" };
                return new Result()
                {
                    IsSuccess = false,
                    Errors = error
                };
            }
            SaveUserToFile(user);
            // If validation is successful, create user and return success result
            return new Result
            {
                // Success = true,
                // Message = "User created successfully",
                // Data = user
            };

            // If validation fails, return failure result with error message
            //return new Result
            //{
            //    Success = false,
            //    Message = "Validation failed"
            //};
        }
        private static void SaveUserToFile(User user)
        {
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter("users.txt", true))
            {
                // Write the user information to the file
                writer.WriteLine($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}");
            }
        }
        private static User CreateUserObject(string name, string email, string address, string phone, string userType, string money)
        {
            return new User
            {
                Name = name,
                Email = Utils.NormalizeEmail(email),
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };
        }
    }
}
