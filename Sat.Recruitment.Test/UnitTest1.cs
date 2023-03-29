using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Logic.Business;
using Sat.Recruitment.Api.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void UserIsCreatedSuccessfully()
        {
            var logger = new Logger<UsersController>(NullLoggerFactory.Instance);
            var userController = new UsersController(new Users(), logger);

            var result = userController.CreateUser(new UserRequest { Name = "Mike", 
                Email = "mike@gmail.com", 
                Address = "Av. Juan G", 
                Phone = "+349 1122354215", 
                UserType = "Normal", 
                Money = 124 }).Result;


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void DuplicateUser()
        {
            var logger = new Logger<UsersController>(NullLoggerFactory.Instance);
            var userController = new UsersController(new Users(), (ILogger<UsersController>)logger);

            var result = userController.CreateUser(new UserRequest
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            }).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void RequiredFieldsMissing()
        {
            var logger = new Logger<UsersController>(NullLoggerFactory.Instance);
            var userController = new UsersController(new Users(), (ILogger<UsersController>)logger);

            var result = userController.CreateUser(new UserRequest
            {
                Name = "",
                Email = "test@gmail.com",
                Address = "Av. Test",
                Phone = "1122354215",
                UserType = "SuperUser",
                Money = 124
            }).Result;

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WrongEmailFormat()
        {
            var logger = new Logger<UsersController>(NullLoggerFactory.Instance);
            var userController = new UsersController(new Users(), (ILogger<UsersController>)logger);

            var result = userController.CreateUser(new UserRequest
            {
                Name = "Test",
                Email = "testgmail.com",
                Address = "Av. Test",
                Phone = "1122354215",
                UserType = "Premium",
                Money = 124
            }).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("Email has no the correct format", result.Errors);
        }

        [Fact]
        public void UserTypeNotExists()
        {
            var logger = new Logger<UsersController>(NullLoggerFactory.Instance);
            var userController = new UsersController(new Users(), (ILogger<UsersController>)logger);

            var result = userController.CreateUser(new UserRequest
            {
                Name = "Test",
                Email = "test@gmail.com",
                Address = "Av. Test",
                Phone = "1122354215",
                UserType = "TestigType",
                Money = 124
            }).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("UserType not exists.", result.Errors);
        }
    }
}
