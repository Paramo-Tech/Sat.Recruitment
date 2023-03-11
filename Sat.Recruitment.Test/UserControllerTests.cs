using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Global.Interfaces;
using Sat.Recruitment.Global.WebContracts;
using Sat.Recruitment.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTests
    {
        private readonly IUsersService _usersService;

        public UserControllerTests()
        {
            _usersService = new UsersService();
        }

        [Fact]
        public async Task CreateUser_NewUser_Normal_Succeed()
        {
            var userController = new UsersController(_usersService);

            var userMoney = 124;
            var newUser = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", userMoney.ToString());

            var result = await userController.CreateUser(newUser);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);

            // Using initial money value to test calculation
            var gif = userMoney * Convert.ToDecimal(0.12);
            Assert.Equal(newUser.Money, userMoney + gif);
        }

        [Fact]
        public async Task CreateUser_NewUser_NormalWithLessMoney_Succeed()
        {
            var userController = new UsersController(_usersService);

            var userMoney = 50;
            var newUser = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", userMoney.ToString());

            // User Money will be updated
            var result = await userController.CreateUser(newUser);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);

            // Using initial money value to test calculation
            var gif = userMoney * Convert.ToDecimal(0.8);
            Assert.Equal(newUser.Money, userMoney + gif);
        }

        [Fact]
        public async Task CreateUser_NewUser_SuperUser_Succeed()
        {
            var userController = new UsersController(_usersService);

            var userMoney = 140;
            var newUser = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "SuperUser", userMoney.ToString());

            // User Money will be updated
            var result = await userController.CreateUser(newUser);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);

            // Using initial money value to test calculation
            var gif = userMoney * Convert.ToDecimal(0.20);
            Assert.Equal(newUser.Money, userMoney + gif);
        }

        [Fact]
        public async Task CreateUser_NewUser_Premium_Succeed()
        {
            var userController = new UsersController(_usersService);

            var userMoney = 180;
            var newUser = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Premium", userMoney.ToString());

            // User Money will be updated
            var result = await userController.CreateUser(newUser);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);

            // Using initial money value to test calculation
            var gif = userMoney * 2;
            Assert.Equal(newUser.Money, userMoney + gif);
        }

        [Fact]
        public async Task CreateUser_InvalidEmail_Error()
        {
            var userController = new UsersController(_usersService);

            var newUser = new User("Mike", "wrongemail", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = await userController.CreateUser(newUser);

            Assert.False(result.IsSuccess);
            Assert.Equal("Not valid Email", result.Errors);
        }

        [Fact]
        public async Task CreateUser_Duplicated_Error()
        {
            var userController = new UsersController(_usersService);

            var newUser = new User("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = await userController.CreateUser(newUser);

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}