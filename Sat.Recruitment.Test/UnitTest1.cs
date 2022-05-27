using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.ApiModels;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userController = new UsersController(new UserService(new StoreServices()));

            var newUser = new CreateUserRequest()
            {
                Name = "Mike", 
                Email = "mike@gmail.com", 
                Address = "Av. Juan G", 
                Phone = "+349 1122354215",
                UserType =  UserType.Normal,
                Money = 124
            };

            var result = userController
                .CreateUser(newUser).Result;


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController(new UserService(new StoreServices()));
            
            var newUser = new CreateUserRequest()
            {
                Name = "Agustina", 
                Email = "Agustina@gmail.com", 
                Address = "Av. Juan G", 
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };


            var result = userController.CreateUser(newUser).Result;
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}