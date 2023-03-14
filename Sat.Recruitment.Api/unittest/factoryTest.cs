using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Sat.Recruitment.Api.unittest
{
    [TestClass]
    public class CreateUserTests
    {
        private Mock<IUserFactory> _factoryMock;
        private Mock<IUserCreator> _creatorMock;
        private List<User> _users;

        [TestInitialize]
        public void Setup()
        {
            _factoryMock = new Mock<IUserFactory>();
            _creatorMock = new Mock<IUserCreator>();
            _users = new List<User>();
        }

        [TestMethod]
        public async Task CreateUser_Success()
        {
            // Arrange
            var name = "John Smith";
            var email = "john.smith@example.com";
            var address = "123 Main St";
            var phone = "555-1234";
            var userType = "Normal";
            var money = "50";
            var result = new Result { IsSuccess = true, Errors = "User Created" };

            _factoryMock.Setup(x => x.GetUserCreator(userType)).Returns(_creatorMock.Object);
            _creatorMock.Setup(x => x.CreateUser(name, email, address, phone, decimal.Parse(money))).Returns(Task.CompletedTask);

            var target = new MyClass(_factoryMock.Object, _users);

            // Act
            var actual = await target.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.AreEqual(result.IsSuccess, actual.IsSuccess);
            Assert.AreEqual(result.Errors, actual.Errors);
        }

        [TestMethod]
        public async Task CreateUser_ValidationErrors()
        {
            // Arrange
            var name = "";
            var email = "john.smith@example.com";
            var address = "123 Main St";
            var phone = "555-1234";
            var userType = "Normal";
            var money = "50";
            var errors = "Name is required.";

            var target = new MyClass(_factoryMock.Object, _users);

            // Act
            var actual = await target.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.AreEqual(errors, actual.Errors);
        }

        [TestMethod]
        public async Task CreateUser_DuplicateUser()
        {
            // Arrange
            var name = "John Smith";
            var email = "john.smith@example.com";
            var address = "123 Main St";
            var phone = "555-1234";
            var userType = "Normal";
            var money = "50";
            var errors = "The user is duplicated";

            _users.Add(new User { Name = name, Email = email, Address = address, Phone = phone, UserType = userType, Money = decimal.Parse(money) });

            var target = new MyClass(_factoryMock.Object, _users);

            // Act
            var actual = await target.CreateUser(name, email, address, phone, userType, money);

            // Assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.AreEqual(errors, actual.Errors);
        }
    }
}
