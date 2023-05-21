using Moq;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Respositories;
using Sat.Recruitment.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task SaveUser_ValidUser_ReturnsNewUser()
        {
            // Arrange
            var newUser = new UserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124m
            };

            var expectedUser = new User
            {
                Address = newUser.Address,
                Email = newUser.Email,
                Money = newUser.Money,
                Name = newUser.Name,
                Phone = newUser.Phone,
                UserType = UserType.Normal
            };

            _mockUserRepository.Setup(r => r.ReadUsers()).Returns(new List<User>());
            _mockUserRepository.Setup(r => r.CreateUser(It.IsAny<User>()));

            // Act
            var result = await _userService.SaveUser(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser, result);
        }

        [Fact]
        public void SaveUser_DuplicateUser_ThrowsException()
        {
            // Arrange
            var newUser = new UserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124m
            };

            var existingUser = new User
            {
                Address = newUser.Address,
                Email = newUser.Email,
                Money = newUser.Money,
                Name = newUser.Name,
                Phone = newUser.Phone,
                UserType = UserType.Normal
            };

            _mockUserRepository.Setup(r => r.ReadUsers()).Returns(new List<User> { existingUser });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _userService.SaveUser(newUser));
        }
    }
}