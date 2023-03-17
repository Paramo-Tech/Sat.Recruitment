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
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
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
        Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Sat.Recruitment.Api.services.IUserService" />
    public class UserService : IUserService
    {

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

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
        public Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = new List<string>();
            Utils.ValidateErrors(name, email, address, phone,money, ref errors);

            if (errors.Any())
            {
                return Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                });
            }


            AUserFactory factory = new UserFactory(_logger);
            IUserCreator creator = factory.GetUserCreator(userType);
            var response = creator.CreateUser(name, email, address, phone, userType, decimal.Parse(money));

            return Task.FromResult(response.Result);
        }


    }
}
