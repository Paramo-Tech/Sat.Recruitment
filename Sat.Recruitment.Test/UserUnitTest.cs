using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Data.Implementations;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Services.Implementations;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserUnitTest
    {
        [Fact]
        public void Test1()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var userController = new UsersController(userService);

            var user = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124

            };

            var result = userController.CreateUser(user).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var userController = new UsersController(userService);
            var user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124

            };
            var result = userController.CreateUser(user).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
