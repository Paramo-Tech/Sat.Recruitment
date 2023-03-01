using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Repository;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTest
    {
        [Fact]
        public void Test1()
        {

            //var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


            //Assert.Equal(true, result.IsSuccess);
            //Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            //var userController = new UsersController();

            //var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


            //Assert.Equal(false, result.IsSuccess);
            //Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
