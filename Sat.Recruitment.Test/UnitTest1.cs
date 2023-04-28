using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTOs;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            //var userController = new UsersController();
            //var userDto = new UserDTO("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);

            //var result = await (await userController.CreateUser(userDto)).Result;


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
