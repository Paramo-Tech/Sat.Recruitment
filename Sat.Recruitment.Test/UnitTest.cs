using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Entities;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest
    {
        [Fact]
        public void TestUserCreated()
        {
            var userController = new UserController();
            User user = new User
            {
                Name = "Ricardo",
                Phone = "11111111",
                Email = "ricardo@gmail.com",
                Address = "Calle 19",
                UserType = "SuperUser",
                Money = 120
            };
            var result = userController.CreateUser(user).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("Item Created", result.Message);


        }

        [Fact]
        public void TestUserDuplicated()
        {
            var userController = new UserController();
            User user = new User
            {
                Name = "Agustina",
                Phone = "+349 1122354215",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = 124
            };
            var result = userController.CreateUser(user).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("The item is duplicated", result.Message);


        }



        [Fact]
        public void TestInvalidModel()
        {
            var userController = new UserController();
            User user = new User
            {
                Name = string.Empty,
                Phone = "+349 1122354215",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = 124
            };
            var result = userController.CreateUser(user).Result;

            Assert.False(result.IsSuccess);
            Assert.Contains("Invalid model", result.Message);


        }
    }
}
