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
    /// <summary>
    /// 
    /// </summary>
    public interface IUserCreator
    {
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="address">The address.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="money">The money.</param>
        /// <returns></returns>
        Task<Result> CreateUser(string name, string email, string address, string phone, string userType, decimal money);
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class AUserFactory
    {
        /// <summary>
        /// Gets the user creator.
        /// </summary>
        /// <param name="userType">Type of the user.</param>
        /// <returns></returns>
        public abstract IUserCreator GetUserCreator(string userType);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Sat.Recruitment.Api.utilities.AUserFactory" />
    public class UserFactory : AUserFactory
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static ILogger<UserService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFactory"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UserFactory(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Gets the user creator.
        /// </summary>
        /// <param name="userType">Type of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid user type: {userType}</exception>
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
                    throw new ArgumentException($"Invalid user type.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Sat.Recruitment.Api.utilities.IUserCreator" />
        public class NormalUserCreator : IUserCreator
        {

            /// <summary>
            /// Creates the user.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="email">The email.</param>
            /// <param name="address">The address.</param>
            /// <param name="phone">The phone.</param>
            /// <param name="userType">Type of the user.</param>
            /// <param name="money">The money.</param>
            /// <returns></returns>
            public Task<Result> CreateUser(string name, string email, string address, string phone, string userType,
                decimal money)
            {
                var newUser = CreateUserObject(name, email, address, phone, userType, money);

                if ((money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = (money) * percentage;
                    newUser.Money += gif;
                }
                else if ((money) > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = (money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }

                return ValidateAndCreateUser(newUser);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Sat.Recruitment.Api.utilities.IUserCreator" />
        public class SuperUserCreator : IUserCreator
        {
            /// <summary>
            /// Creates the user.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="email">The email.</param>
            /// <param name="address">The address.</param>
            /// <param name="phone">The phone.</param>
            /// <param name="userType">Type of the user.</param>
            /// <param name="money">The money.</param>
            /// <returns></returns>
            public Task<Result> CreateUser(string name, string email, string address, string phone, string userType,
                decimal money)
            {
                var newUser = CreateUserObject(name, email, address, phone, userType, money);

                if ((money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = (money) * percentage;
                    newUser.Money += gif;
                }

                return ValidateAndCreateUser(newUser);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Sat.Recruitment.Api.utilities.IUserCreator" />
        public class PremiumUserCreator : IUserCreator
        {
            /// <summary>
            /// Creates the user.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="email">The email.</param>
            /// <param name="address">The address.</param>
            /// <param name="phone">The phone.</param>
            /// <param name="userType">Type of the user.</param>
            /// <param name="money">The money.</param>
            /// <returns></returns>
            public Task<Result> CreateUser(string name, string email, string address, string phone, string userType,
                decimal money)
            {
                var newUser = CreateUserObject(name, email, address, phone, userType, money);

                if (money > 100)
                {
                    var gif = (money) * 2;
                    newUser.Money += gif;
                }

                return ValidateAndCreateUser(newUser);
            }
        }
        /// <summary>
        /// Validates the and create user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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
            _logger.LogInformation("User Created");
            return new Result
            {
                IsSuccess = true
            };

            
        }
        /// <summary>
        /// Saves the user to file.
        /// </summary>
        /// <param name="user">The user.</param>
        private static void SaveUserToFile(User user)
        {
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter("users.txt", true))
            {
                // Write the user information to the file
                writer.WriteLine($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}");
            }
        }
        /// <summary>
        /// Creates the user object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="address">The address.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="money">The money.</param>
        /// <returns></returns>
        private static User CreateUserObject(string name, string email, string address, string phone, string userType, decimal money)
        {

            return new User
            {
                Name = name,
                Email = Utils.NormalizeEmail(email),
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };
        }
    }
}
