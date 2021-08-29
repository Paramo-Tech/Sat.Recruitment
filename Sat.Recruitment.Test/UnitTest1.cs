using System;
using System.Dynamic;
using Sat.Recruitment.Api.Models;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;

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

            var result = userController.CreateUser(
                new UserRequestData()
                {
                    name = "Mike",
                    email = "mike@gmail.com",
                    address = "Av. Juan G",
                    phone = "+349 1122354215",
                    userType = UserType.Normal,
                    money = "124"
                }).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();

            var result = userController.CreateUser(new UserRequestData()
            {
                name = "Agustina",
                email = "Agustina@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = UserType.Normal,
                money = "124"
            }).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
