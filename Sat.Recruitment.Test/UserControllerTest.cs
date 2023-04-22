using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        [Fact]
        public void TestUserCreatedOk()
        {
            var userController = new UsersController(new UserService());

            var result = userController.Post(
                new Api.Models.User
                {
                    Name = "Mike",
                    Email = "mike@gmail.com",
                    Address = "Av. Juan G",
                    Phone = "+349 1122354215",
                    UserType = Api.Enums.UserType.Normal,
                    Money = 124
                }).Result;


            Assert.NotNull(result);
            Assert.Equal(200, (result as StatusCodeResult).StatusCode);

        }

        [Fact]
        public void TestUserDuplicated()
        {
            var userController = new UsersController(new UserService());

            var result = userController.Post(
                new Api.Models.User
                {
                    Name = "Agustina",
                    Email = "Agustina@gmail.com",
                    Address = "Av. Juan G",
                    Phone = "+349 1122354215",
                    UserType = Api.Enums.UserType.Normal,
                    Money = 124
                }
                ).Result;

            Assert.NotNull(result);
            Assert.Equal(409, (result as StatusCodeResult).StatusCode);
            
        }
    }
}
