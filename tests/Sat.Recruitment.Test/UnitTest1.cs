using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTO;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userController = new UsersController();

            CreateUserRequest createUserRequest = new CreateUserRequest()
            {
                name = "Mike",
                email = "mike@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            };

            var result = userController.CreateUser(createUserRequest).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();

            CreateUserRequest createUserRequest = new CreateUserRequest()
            {
                name = "Agustina",
                email = "Agustina@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            };

            var result = userController.CreateUser(createUserRequest).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
