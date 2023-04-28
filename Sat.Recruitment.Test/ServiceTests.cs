using Moq;
using Sat.Recruitment.Api.Business.Entities;
using Sat.Recruitment.Api.Business.Services;
using Sat.Recruitment.Api.Data.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class ServiceTests
    {
        private readonly Mock<IRepository<User>> _userRepositoryMock;

        public ServiceTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
        }

        [Fact]
        public async Task CreateUser_CreatedSuccessfully()
        {
            // Arrange
            var userService = new UserService(_userRepositoryMock.Object);
            var validUser = new User { Name = "Ricardo", Email = "ricardo@gmail.com", Address = "chan 12", Phone = "123-456-7890", UserType = UserType.Normal, Money = 500 };

            _userRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await userService.Create(validUser);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateUser_NotCreated()
        {
            // Arrange
            var userService = new UserService(_userRepositoryMock.Object);
            var validUser = new User
            {
                Name = "Tano Romano",
                Email = "tano.romano@example.com",
                Address = "Calle Falsa 123",
                Phone = "456-789-1230",
                UserType = UserType.SuperUser,
                Money = 1000
            };

            _userRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await userService.Create(validUser);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreateUser_InvalidUserType()
        {
            // Arrange
            var userService = new UserService(_userRepositoryMock.Object);
            var invalidUser = new User { Name = "Ricardo", Email = "ricardo@gmail.com", Address = "chan 12", Phone = "123-456-7890", UserType = (UserType) (-1), Money = 500 };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => userService.Create(invalidUser));
        }
    }
}
