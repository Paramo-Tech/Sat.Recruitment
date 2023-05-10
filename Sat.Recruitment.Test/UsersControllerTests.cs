using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.BLL;
using Sat.Recruitment.BLL.Dto;
using Sat.Recruitment.BLL.interfaces;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    [TestFixture]
    public class UserControllerTests
    {
        private UsersController _controller;
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<UsersController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task CreateUser_ValidUser_ReturnsSuccessResult()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@example.com";
            var address = "123 Main St";
            var phone = "123-456-7890";
            var userType = "regular";
            var money = "1000.00";
            var expected = new Result() { IsSuccess = true };

            var newUser = new CreateUserDTO
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            _userServiceMock.Setup(x => x.CreateUser(newUser))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task CreateUser_InvalidUser_ReturnsErrorResult()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@example.com";
            var address = "123 Main St";
            var phone = "123-456-7890";
            var userType = "regular";
            var money = "invalid_money_value";
            var expectedError = "The input is not a valid decimal";
            var expected = new Result() { IsSuccess = false, Errors = expectedError };

            var newUser = new CreateUserDTO
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = 0
            };

            _userServiceMock.Setup(x => x.CreateUser(newUser))
                .ThrowsAsync(new FormatException(expectedError));

            // Act
            var result = await _controller.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.AreEqual(expected.IsSuccess, result.IsSuccess);
            Assert.AreEqual(expected.Errors, result.Errors);
        }
    }


}
