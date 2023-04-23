using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UsersControllerTests")]
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();

            _controller = new UsersController(_loggerMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async Task PostValidUserReturnsOk()
        {
            
            var user = new User
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Address = "123 Main St",
                Phone = "555-1234",
                UserType = UserType.Normal,
                Money = 100
            };

            _userServiceMock.Setup(x => x.Create(user)).Returns(true);

            var result = await _controller.Post(user);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Created", okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value, null));
        }

        [Fact]
        public async Task PostInvalidUserReturnsBadRequest()
        {
            var user = new User
            {
                Name = "John Doe",
                Email = "johndoeexample.com", // Email address is missing '@'
                Address = "123 Main St",
                Phone = "555-1234",
                UserType = UserType.Normal,
                Money = 100
            };

            _controller.ModelState.AddModelError("Email", "The Email field is not a valid e-mail address.");

            var result = await _controller.Post(user);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostDuplicateUserReturnsConflict()
        {

            var user = new User
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Address = "123 Main St",
                Phone = "555-1234",
                UserType = UserType.Normal,
                Money = 100
            };

            _userServiceMock.Setup(x => x.Create(user)).Returns(false);

            var result = await _controller.Post(user);

            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("The user is duplicated", conflictResult.Value.GetType().GetProperty("message").GetValue(conflictResult.Value, null));
        }
    }

}
