using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sat.Recruitment.Api.DbContext;
using Sat.Recruitment.Api.Handlers.CommandHandlers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Requests.Commands;
using Sat.Recruitment.Api.Services;
using Xunit;
using FluentValidation.TestHelper;
using FluentValidation;
using FluentValidation.Results;

namespace Sat.Recruitment.Test
{
    public class AddUserTest
    {
        [Fact]
        public async Task CreateUserHandler_Creates_User_In_Repository()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<User>>();
            var handler = new AddUserCommandHandler(mockRepository.Object);

            var command = new AddUserCommand { name = "John", email = "john@example.com", address = "ever green 123", phone = "12341234", userType = "Premium", money = "1234" };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var user = new AddUserCommand { name = string.Empty, address = "ever green 1234", email = "nico@gmail.com", phone = "13241324", userType = "Premium", money = "1234"};
            var validator = new AddUserCommandValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Not_Valid()
        {
            // Arrange
            var user = new AddUserCommand { name = "nico", address = "ever green 1234", email = "nicogmail.com", phone = "13241324", userType = "Premium", money = "1234" };
            var validator = new AddUserCommandValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_UserType_Is_Not_Valid()
        {
            // Arrange
            var user = new AddUserCommand { name = "iamuser", address = "ever green 1234", email = "nico@gmail.com", phone = "13241324", userType = "Old", money = "1234" };
            var validator = new AddUserCommandValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void UserFactory_Should_Return_Instance_Of_Premium_When_UserTupe_Is_Premium()
        {
            // Arrange
            var request = new AddUserCommand { name = "John", email = "john@example.com", address = "ever green 123", phone = "12341234", userType = "Premium", money = "1234" };

            // Act
            var user = UserFactory.CreateUser(request.userType);

            // Assert
            Assert.IsType<Premium>(user);
        }
    }
}
