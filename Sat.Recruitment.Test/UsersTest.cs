using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersTest
    {
        readonly UserService _userService;
        public UsersTest()
        {
            _userService = new UserService();
        }

        [Theory]
        [InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        public void CreateUser(string name, string email, string address, string phone, string userType, decimal money)
        {
            var userController = new UsersController(_userService);
            User user = new User() { name = name, email = email, address = address, phone = phone, userType = userType, money = money };
            var result = userController.CreateUser(user);


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Theory]
        [InlineData("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        public void DuplicatedUser(string name, string email, string address, string phone, string userType, decimal money)
        {
            var userController = new UsersController(_userService);
            User user = new User() { name = name, email = email, address = address, phone = phone, userType = userType, money = money };
            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Theory]
        [InlineData("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215")]
        public void GetAllUsers(string name, string email, string address, string phone)
        {
            var userController = new UsersController(_userService);
            var result = userController.GetAllUsers();
            Assert.NotEmpty(result);
            Assert.True(result.Exists(x => x.email == email || x.phone == phone || x.name == name || x.address == address));
        }

    }
}
