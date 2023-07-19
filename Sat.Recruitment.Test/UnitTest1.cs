using System;
using System.Dynamic;

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

            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();

            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
        }
        [Fact]
        public void TestEmptyNameField()
        {
            var userController = new UsersController();

            var result = userController.CreateUser("", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The name is required", result.Message);
        }
        [Fact]
        public void TestEmptyEmailField()
        {
            var userController = new UsersController();

            var result = userController.CreateUser("Agustina", "", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The email is required", result.Message);
        }
        [Fact]
        public void TestEmptyAddressField()
        {
            var userController = new UsersController();

            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "", "+349 1122354215", "Normal", "124").Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The address is required", result.Message);
        }
        [Fact]
        public void TestEmptyPhoneField()
        {
            var userController = new UsersController();

            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "", "Normal", "124").Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The phone is required", result.Message);
        }
        [Fact]
        public void TestEmptyPhoneField()
        {
            var userController = new UsersController();

            var result = userController.CreateUser("", "", "", "", "Normal", "124").Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The name is required. The email is required. The address is required. The phone is required", result.Message);
        }
    }
}
