using FluentAssertions;
using Moq;
using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;
using Sat.Recruitment.Kernel.Features.Users.CreateUserCommand;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.CommandHandlers
{
    public class CreateUserCommandHandlerTests
    {
        private Mock<IUserService> _userServiceMock;

        private Mock<IUserCalculateGiftValue> _userCalculateGiftValueMock;

        public CreateUserCommandHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userCalculateGiftValueMock = new Mock<IUserCalculateGiftValue>();
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenValidCommand()
        {
            // Arrange
            _userServiceMock.Setup(x => x.AddAsync(It.IsAny<IUser>()));
            _userServiceMock.Setup(x => x.GetAsync())
                .Returns(Task.FromResult(new List<IUser>().AsEnumerable()));
            _userCalculateGiftValueMock.Setup(x => x.GetGiftValue(It.IsAny<string>(), It.IsAny<decimal>()))
                .Returns(0.2m);

            var handler = new CreateUserHandler(_userServiceMock.Object, _userCalculateGiftValueMock.Object);
            var command = new CreateUserRequest
            {
                Name = "Hector Juan Perez Martinez",
                Address = "Calle luna, calle sol",
                Email = "hector.lavoe@fania.com",
                Money = 100,
                Phone = "555-6543",
                UserType = "Normal"
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

        }

        [Fact]
        public async Task Handle_ShouldReturnUserDuplicatedException_NameAndAddress()
        {
            // Arrange
            var users = new List<IUser>() { new User() { Name = "Hector Juan Perez Martinez", Address = "Calle luna, calle sol" } };
            _userServiceMock.Setup(x => x.AddAsync(It.IsAny<IUser>()));
            _userServiceMock.Setup(x => x.GetAsync())
                .Returns(Task.FromResult(users.AsEnumerable()));
            _userCalculateGiftValueMock.Setup(x => x.GetGiftValue(It.IsAny<string>(), It.IsAny<decimal>()))
                .Returns(0.2m);

            var handler = new CreateUserHandler(_userServiceMock.Object, _userCalculateGiftValueMock.Object);
            var command = new CreateUserRequest
            {
                Name = "Hector Juan Perez Martinez",
                Address = "Calle luna, calle sol",
                Email = "hector.lavoe@fania.com",
                Money = 100,
                Phone = "555-6543",
                UserType = "Normal"
            };

            // Act
            var result = await Assert.ThrowsAsync<UserDuplicatedException>(() => handler.Handle(command, default));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDuplicatedException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUserDuplicatedException_EmailOrPhone()
        {
            // Arrange
            var users = new List<IUser>() { new User() { Email = "hector.lavoe@fania.com", Phone = "555-6543" } };
            _userServiceMock.Setup(x => x.AddAsync(It.IsAny<IUser>()));
            _userServiceMock.Setup(x => x.GetAsync())
                .Returns(Task.FromResult(users.AsEnumerable()));
            _userCalculateGiftValueMock.Setup(x => x.GetGiftValue(It.IsAny<string>(), It.IsAny<decimal>()))
                .Returns(0.2m);

            var handler = new CreateUserHandler(_userServiceMock.Object, _userCalculateGiftValueMock.Object);
            var command = new CreateUserRequest
            {
                Name = "Hector Juan Perez Martinez",
                Address = "Calle luna, calle sol",
                Email = "hector.lavoe@fania.com",
                Money = 100,
                Phone = "555-6543",
                UserType = "Normal"
            };

            // Act
            var result = await Assert.ThrowsAsync<UserDuplicatedException>(() => handler.Handle(command, default));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDuplicatedException>();
        }
    }
}
