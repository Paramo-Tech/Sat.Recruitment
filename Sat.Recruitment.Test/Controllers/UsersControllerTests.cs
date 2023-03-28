using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Core.DataTransferObjects;
using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;
using Sat.Recruitment.Kernel.Features.Users.CreateUserCommand;
using Sat.Recruitment.Kernel.Features.Users.GetUserQueryByIdCommand;
using Sat.Recruitment.Kernel.Features.Users.GetUsersQueryCommand;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;

        private readonly Mock<IMediator> _mediator;

        private readonly Mock<IValidator<CreateUserRequest>> _validator;

        public UsersControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _validator = new Mock<IValidator<CreateUserRequest>>();
            _usersController = new UsersController(_mediator.Object, _validator.Object);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var id = "1";
            _mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQueryRequest>(), default))
                .ReturnsAsync(new User { Id = id });

            // Act
            var result = await _usersController.GetUserById(id.ToString());

            // Assert
            result.Should().BeOfType<ActionResult<IResponseResult<IUser>>>();
            result.Value.Should().NotBeNull();
            result.Value.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var id = 4;

            _mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQueryRequest>(), default))
                .ReturnsAsync((User)null);

            // Act
            var result = await _usersController.GetUserById(id.ToString());

            // Assert
            result.Value.Result.Should().BeNull();

        }

        [Fact]
        public async Task GetUsers_ShouldReturnAllUsers()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetUsersQueryRequest>(), default))
                .ReturnsAsync(GetListWithData());

            // Act
            var result = await _usersController.GetUsers();

            // Assert
            result.Should().BeOfType<ActionResult<IResponseResult<List<IUser>>>>();
            result.Value.Should().NotBeNull();
            result.Value.IsSuccess.Should().BeTrue();
            result.Value.Result.Should().HaveCount(3);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnNull_WhenIsNotValidRequest()
        {
            //Arrange
            var request = new CreateUserRequest();

            _mediator.Setup(x => x.Send(It.IsAny<CreateUserRequest>(), default))
                .ReturnsAsync(1);
            _validator.Setup(x => x.ValidateAsync(It.IsAny<CreateUserRequest>(), default))
                .Returns(Task.FromResult(new ValidationResult() { Errors = new List<ValidationFailure>() { new ValidationFailure() { ErrorCode = "1", ErrorMessage = "There is an error." } } }));
            
            // Act
            var result = await Assert.ThrowsAsync<ValidationException>(() => _usersController.CreateUser(request));

            // Assert
            result.Should().BeOfType<ValidationException>();
        }

        [Fact]
        public async Task CreateUser_ShouldReturnTrue_WhenSuccess()
        {
            //Arrange
            var request = new CreateUserRequest();

            _mediator.Setup(x => x.Send(It.IsAny<CreateUserRequest>(), default))
                .ReturnsAsync(1);
            _validator.Setup(x => x.ValidateAsync(It.IsAny<CreateUserRequest>(), default))
                .Returns(Task.FromResult(new ValidationResult() { }));

            // Act
            var result = await _usersController.CreateUser(request);

            // Assert
            result.Should().BeOfType<ResponseResultOk<string>>();
            result.IsSuccess.Should().BeTrue();
        }

        private List<IUser> GetListWithData()
        {
            List<IUser> users = new List<IUser>();
            users.AddRange(new List<User>
            {
                new User { Id = "1", Name = "John", Address = "Doe", Email = "johndoe@example.com" },
                new User { Id = "2", Name = "Jane", Address = "Doe", Email = "janedoe@example.com" },
                new User { Id = "3", Name = "Bob", Address = "Smith", Email = "bobsmith@example.com" }
            });
            return users;
        }
    }
}
