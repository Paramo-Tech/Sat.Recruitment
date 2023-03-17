using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sat.Recruitment.Api.services;
using Sat.Recruitment.Api.utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    internal class UserFactoryTest
    {
        private Mock<ILogger<UserService>> _loggerMock;
        private UserFactory _userFactory;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
            _userFactory = new UserFactory(_loggerMock.Object);
        }

        [Test]
        public async Task NormalUserCreator_WithMoneyGreaterThan100_ShouldAddGift()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@example.com";
            var address = "123 Main St";
            var phone = "+1 (123) 456-7890";
            var userType = "Normal";
            var money = 150m;

            // Act
            var result = await _userFactory.GetUserCreator(userType).CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.User.Money, Is.EqualTo(168m)); // 150 + (150 * 0.12)
        }

        [Test]
        public async Task NormalUserCreator_WithMoneyGreaterThan10_ShouldAddGift()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@example.com";
            var address = "123 Main St";
            var phone = "+1 (123) 456-7890";
            var userType = "Normal";
            var money = 15m;

            // Act
            var result = await _userFactory.GetUserCreator(userType).CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.User.Money, Is.EqualTo(27m)); // 15 + (15 * 0.8)
        }

        [Test]
        public async Task SuperUserCreator_WithMoneyGreaterThan100_ShouldAddGift()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@example.com";
            var address = "123 Main St";
            var phone = "+1 (123) 456-7890";
            var userType = "SuperUser";
            var money = 150m;

            // Act
            var result = await _userFactory.GetUserCreator(userType).CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.User.Money, Is.EqualTo(180m)); // 150 + (150 * 0.20)
        }

        [Test]
        public async Task PremiumUserCreator_WithMoneyGreaterThan100_ShouldNotAddGift()
        {
            // Arrange
            var name = "John Doe";
            var email = "johndoe@example.com";
            var address = "123 Main St";
            var phone = "+1 (123) 456-7890";
            var userType = "Premium";
            var money = 150m;

            // Act
            var result = await _userFactory.GetUserCreator(userType).CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.User.Money, Is.EqualTo(450m));
        }
    }
}
