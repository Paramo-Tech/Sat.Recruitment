using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        #region Create new users

        [Theory]
        [InlineData("Rodrigo", "rocampo67@gmail.com", "+595985209533", "Av Pueblo 221", "Normal", 4500)]
        [InlineData("Liz", "lizocampo91@gmail.com", "+595971234567", "Avda Pueblo 221", "Premium", 5000)]
        public void CreateUser_Successfully(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        #endregion

        #region Validate required data

        [Theory]
        [InlineData("", "Juan2@marmol.com", "+5491154762313", "Peru 2465", "Normal", 1234)]
        [InlineData(null, "Franco.Perez2@gmail.com", "+534645213543", "Alvear y Colombres 2", "Premium", 112234)]
        public void CreateUser_ValidateRequieredName(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Contains("The name is required", result.Errors);
        }

        [Theory]
        [InlineData("Juan 2", null, "+5491154762313", "Peru 2465", "Normal", 1234)]
        [InlineData("Franco 2", "", "+534645213543", "Alvear y Colombres 2", "Premium", 112234)]
        public void CreateUser_ValidateRequieredEmail(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Contains("The email is required", result.Errors);
        }

        [Theory]
        [InlineData("Juan 2", "Juan2@marmol.com", "+5491154762313", "", "Normal", 1234)]
        [InlineData("Franco 2", "Franco.Perez2@gmail.com", "+534645213543", null, "Premium", 112234)]
        public void CreateUser_ValidateRequieredAddress(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("The address is required", result.Errors);
        }

        [Theory]
        [InlineData("Juan 2", "Juan2@marmol.com", null, "Peru 2465", "Normal", 1234)]
        [InlineData("Franco 2", "Franco.Perez2@gmail.com", "", "Alvear y Colombres 2", "Premium", 112234)]
        public void CreateUser_ValidateRequieredPhone(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("The phone is required", result.Errors);
        }
        #endregion

        #region Validate duplicate data

        [Theory]
        [InlineData("Juan", "Juan2@marmol.com", "+5491154762313", "Peru 2465", "Normal", 1234)]
        [InlineData("Franco", "Franco.Perez2@gmail.com", "+534645213543", "Alvear y Colombres 2", "Premium", 112234)]
        public void CreateUser_ValidateDuplicateName(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        [Theory]
        [InlineData("Juan 2", "Juan@marmol.com", "+5491154762313", "Peru 2465", "Normal", 1234)]
        [InlineData("Franco 2", "Franco.Perez@gmail.com", "+534645213543", "Alvear y Colombres 2", "Premium", 112234)]
        public void CreateUser_ValidateDuplicateEmail(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        [Theory]
        [InlineData("Juan 2", "Juan2@marmol.com", "+5491154762313", "Peru 2464", "Normal", 1234)]
        [InlineData("Franco 2", "Franco.Perez2@gmail.com", "+534645213543", "Alvear y Colombres", "Premium", 112234)]
        public void CreateUser_ValidateDuplicateAddress(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        [Theory]
        [InlineData("Juan 2", "Juan2@marmol.com", "+5491154762312", "Peru 2465", "Normal", 1234)]
        [InlineData("Franco 2", "Franco.Perez2@gmail.com", "+534645213542", "Alvear y Colombres 2", "Premium", 112234)]
        public void CreateUser_ValidateDuplicatePhone(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IUsersService iUsersService = serviceProvider.GetService<IUsersService>();
            UsersController userController = new UsersController(log.Object, iUsersService);

            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = userController.CreateUser(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        #endregion

        #region Validate duplicate user data

        [Theory]
        [InlineData("Juan", "Juan2@marmol.com", "+5491154762313", "Peru 2465", "Normal", 1234)]
        [InlineData("Franco", "Franco.Perez2@gmail.com", "+534645213543", "Alvear y Colombres 2", "Premium", 112234)]
        public void DuplicateUser_ValidateName(string userName, string userEmail, string userPhone, string userAddress, string userType, decimal userMoney)
        {
            UsersService usersService = new UsersService(null);
            User newUser = new User() { Name = userName, Email = userEmail, Phone = userPhone, Address = userAddress, UserType = userType, Money = userMoney };

            var result = usersService.ValidateUserRepo(newUser).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        #endregion

        #region Calculate money gif per user type

        [Fact]
        public void GetMoneyGifNormalOver100()
        {
            string userType = "Normal";
            decimal userMoney = 101;
            decimal percentage = Convert.ToDecimal(0.12);
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void GetMoneyGifNormalOver10()
        {
            string userType = "Normal";
            decimal userMoney = 12;
            decimal percentage = Convert.ToDecimal(0.08);
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void GetMoneyGifNormal10orLess()
        {
            string userType = "Normal";
            decimal userMoney = 5;
            decimal percentage = 0;
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void GetMoneyGifSuperUserOver100()
        {
            string userType = "SuperUser";
            decimal userMoney = 101;
            decimal percentage = Convert.ToDecimal(0.20);
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void GetMoneyGifSuperUser100orLess()
        {
            string userType = "SuperUser";
            decimal userMoney = 100;
            decimal percentage = 0;
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void GetMoneyGifPremiumOver100()
        {
            string userType = "Premium";
            decimal userMoney = 101;
            decimal percentage = Convert.ToDecimal(2);
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void GetMoneyGifPremium100orLess()
        {
            string userType = "Premium";
            decimal userMoney = 100;
            decimal percentage = 0;
            decimal expectedMoney = userMoney + (userMoney * percentage);

            UsersService usersService = new UsersService(null);
            decimal actualMoney = usersService.GetTypeUserMoneyGif(userType, userMoney);

            Assert.Equal(expectedMoney, actualMoney);
        }

        #endregion

    }
    
}
