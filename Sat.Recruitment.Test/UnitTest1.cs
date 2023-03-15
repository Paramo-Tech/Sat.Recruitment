using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.services;

namespace Sat.Recruitment.Test
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<ILogger<UserService>> _loggerMock;
        private Mock<IUserService> _userServiceMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
            _userServiceMock = new Mock<IUserService>();
        }

        [Test]
        public async Task CreateUser_ValidInputs_ShouldReturnSuccessResult()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@test.com";
            var address = "123 Main St";
            var phone = "555-555-1212";
            var userType = "Normal";
            var money = "100.00";

            var expectedResult = new Result { IsSuccess = true };

            _userServiceMock.Setup(x => x.CreateUser(name, email, address, phone, userType, money))
                .ReturnsAsync(expectedResult);

            var controller = new UsersController(_loggerMock.Object);

            // Act
            var result = await controller.CreateUser(name, email, address, phone, userType, money);

            // Assert
            
            Assert.AreEqual(expectedResult.IsSuccess, result.IsSuccess);
        }

        [Test]
        public async Task CreateUser_InvalidInputs_ShouldReturnErrorResult()
        {
            // Arrange
            var name = "";
            var email = "invalid_email";
            var address = "";
            var phone = "";
            var userType = "";
            var money = "invalid_money";

            List<string> expectedErrors = new List<string>()
            { "Name is required.", "Invalid email format.", "Address is required.", "Phone is required.",
                "Invalid money format."
            };
            var expectedResult = new Result { IsSuccess = false, Errors = expectedErrors };

            _userServiceMock.Setup(x => x.CreateUser(name, email, address, phone, userType, money))
                .ReturnsAsync(expectedResult);

            var controller = new UsersController(_loggerMock.Object);

            // Act
            var result = await controller.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.AreEqual(expectedResult.Errors, result.Errors);
        }

        [Test]
        public async Task CreateUser_ThrowsArgumentException_ShouldReturnErrorResult()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@test.com";
            var address = "123 Main St";
            var phone = "555-555-1212";
            var userType = "InvalidType";
            var money = "100.00";

            List<string> expectedErrors = new List<string>() { "Invalid user type" };
            var expectedResult = new Result { IsSuccess = false, Errors = expectedErrors };

            _userServiceMock.Setup(x => x.CreateUser(name, email, address, phone, userType, money))
                .ThrowsAsync(new ArgumentException("Invalid user type"));

            var controller = new UsersController(_loggerMock.Object);

            // Act
            var result = await controller.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
