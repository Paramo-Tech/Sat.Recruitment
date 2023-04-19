using Xunit;
using Sat.Recruitment.Core.Services;
using Sat.Recruitment.Core.Interfaces;
using Moq;
using Sat.Recruitment.Core.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _service;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _service = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnUserCreated_WhenUserIsNotDuplicated()
        {
            // Arrange
            var user = new USER("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);

            _userRepositoryMock.Setup(u => u.IsUserDuplicated(user)).Returns(false);

            // Act
            var result = await _service.Create(user);

            // Assert
            Assert.Equal("User Created", result);
        }

        [Fact]
        public async Task Create_ShouldReturnUserDuplicated_WhenUserIsDuplicated()
        {
            // Arrange
            var user = new  USER("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m); 

            _userRepositoryMock.Setup(u => u.IsUserDuplicated(user)).Returns(true);

            // Act
            var result = await _service.Create(user);

            // Assert
            Assert.Equal("The User is duplicated", result);
        }
    }
}