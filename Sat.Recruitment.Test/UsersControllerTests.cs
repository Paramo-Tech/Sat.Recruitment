using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {

        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var newUser = new UserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124m
            };
            _userServiceMock.Setup(x => x.SaveUser(newUser)).ReturnsAsync(newUser);

            // Act
            var result = await _controller.CreateUser(newUser.Name, newUser.Email, newUser.Address, newUser.Phone, newUser.UserType, newUser.Money.ToString());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultData = Assert.IsType<Result>(okResult.Value);
            Assert.True(resultData.IsSuccess);
        }

        [Fact]
        public async Task CreateUser_InvalidUser_ReturnsBadRequest()
        {
            var newUser = new UserDTO
            {
                Name = "Mike",
                Email = "",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124m
            };

            // Act
            var result = await _controller.CreateUser(newUser.Name, newUser.Email, newUser.Address, newUser.Phone, newUser.UserType, newUser.Money.ToString());

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, actionResult.StatusCode);                        
        }

        [Fact]
        public async Task CreateUser_DuplicatedUser_ReturnsConflictResult()
        {
            // Arrange
            var newUser = new UserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124m
            };

            _userServiceMock.Setup(x => x.SaveUser(It.IsAny<UserDTO>())).Throws(new Exception("The user is duplicated"));

            // Act
            var result = await _controller.CreateUser(newUser.Name, newUser.Email, newUser.Address, newUser.Phone, newUser.UserType, newUser.Money.ToString());

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, conflictResult.StatusCode);
        }
    }
}
