using Moq;
using Sat.Recruitment.BLL.Dto;
using Sat.Recruitment.BLL.Services;
using Sat.Recruitment.DAL.Interfaces;
using Sat.Recruitment.DAL.models;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _repository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _repository = new Mock<IRepository<User>>();
            _userService = new UserService(_repository.Object);
        }

        [Fact]
        public async Task CreateUser_WithValidUser_ReturnsSuccessResult()
        {
            // Arrange
            var user = new CreateUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@test.com",
                Address = "123 Main St",
                Phone = "123-456-7890",
                Money = 100,
                UserType = "Regular"
            };

            _repository.Setup(x => x.Find(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await _userService.CreateUser(user);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async Task CreateUser_WithDuplicateUser_ReturnsErrorResult()
        {
            // Arrange
            var user = new CreateUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@test.com",
                Address = "123 Main St",
                Phone = "123-456-7890",
                Money = 100,
                UserType = "Regular"
            };

            _repository.Setup(x => x.Find(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _userService.CreateUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public async Task CreateUser_WithInvalidUser_ReturnsErrorResult()
        {
            // Arrange
            var user = new CreateUserDTO
            {
                Name = "",
                Email = "",
                Address = "",
                Phone = ""
            };

            _repository.Setup(x => x.Find(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await _userService.CreateUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The name is required", result.Errors);
            Assert.Contains("The email is required", result.Errors);
            Assert.Contains("The address is required", result.Errors);
            Assert.Contains("The phone is required", result.Errors);
        }
    }


}
