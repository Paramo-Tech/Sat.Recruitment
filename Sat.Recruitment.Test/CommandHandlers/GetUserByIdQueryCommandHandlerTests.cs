using FluentAssertions;
using MongoDB.Driver;
using Moq;
using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;
using Sat.Recruitment.Kernel.Features.Users.GetUserQueryByIdCommand;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.CommandHandlers
{
    public class GetUserByIdQueryCommandHandlerTests
    {
        private Mock<IUserService> _userServiceMock;

        public GetUserByIdQueryCommandHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            _userServiceMock.Setup(x => x.GetAsyncById(It.IsAny<object>()))
                .Returns(Task.FromResult((IUser)new User() { Name = "John Smith" }));
            var handler = new GetUserByIdQueryHandler(_userServiceMock.Object);
            var query = new GetUserByIdQueryRequest { Id = "1" };

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            _userServiceMock.Setup(x => x.GetAsyncById(It.IsAny<object>()));
            var handler = new GetUserByIdQueryHandler(_userServiceMock.Object);
            var query = new GetUserByIdQueryRequest { Id = "1" };

            // Act
            var result = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, default));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnArgumentNullException_WhenQueryIsNull()
        {
            // Arrange
            _userServiceMock.Setup(x => x.GetAsyncById(It.IsAny<object>()));
            var handler = new GetUserByIdQueryHandler(_userServiceMock.Object);

            // Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, default));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnArgumentNullException_WhenQueryIdIsNull()
        {
            // Arrange
            _userServiceMock.Setup(x => x.GetAsyncById(It.IsAny<object>()));
            var handler = new GetUserByIdQueryHandler(_userServiceMock.Object);
            var query = new GetUserByIdQueryRequest { Id = null };

            // Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(query, default));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
        }

    }
}
