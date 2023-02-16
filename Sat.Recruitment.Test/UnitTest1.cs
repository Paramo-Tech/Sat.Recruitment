using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.DTO;
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
            UserDTO dto = new UserDTO()
            {
                Name = "Mike",
                Email= "mike@gmail.com",
                Address= "Av. Juan G",
                Phone= "+349 1122354215",
                UserType="Normal",
                Money=124
            };
            var result = userController.CreateUser(dto).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal("User Created", result.Value.ToString());
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();
            UserDTO dto = new UserDTO()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            var result = userController.CreateUser(dto).Result as ConflictObjectResult;

            Assert.NotNull(result);
            Assert.Equal("The user is duplicated", result.Value.ToString());
        }
    }
}
