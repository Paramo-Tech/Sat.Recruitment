using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Repositories;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        private readonly MockFileSystem _fileSystem;

        public UserRepositoryTests()
        {
            _fileSystem = new MockFileSystem();
            _userRepository = new UserRepository();
        }

        [Fact]
        public async Task Create_UserIsNotDuplicated_AddsUserAndReturnsTrue()
        {
            // Arrange
            var newUser = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                Address = "123 Main St",
                UserType = UserType.Normal,
                Money = 100m
            };

            // Act
            var result = await _userRepository.Create(newUser);

            // Assert
            Assert.True(result);
            Assert.Contains(newUser, _userRepository.GetAllAsync().Result);
        }

        [Fact]
        public async Task Create_UserIsDuplicated_DoesNotAddUserAndReturnsFalse()
        {
            // Arrange
            var existingUser = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                Address = "123 Main St",
                UserType = UserType.Normal,
                Money = 100m
            };
            var newUser = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                Address = "123 Main St",
                UserType = UserType.Normal,
                Money = 100m
            };
            await _userRepository.Create(existingUser);

            // Act
            var result = await _userRepository.Create(newUser);

            // Assert
            Assert.False(result);
            Assert.Single(_userRepository.GetAllAsync().Result);
        }

        [Fact]
        public async Task GetAllAsync_UsersExist_ReturnsAllUsers()
        {
            // Arrange
            var user1 = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                Address = "123 Main St",
                UserType = UserType.Normal,
                Money = 100m
            };
            var user2 = new User
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Phone = "987654321",
                Address = "456 Elm St",
                UserType = UserType.Normal,
                Money = 200m
            };
            await _userRepository.Create(user1);
            await _userRepository.Create(user2);

            // Act
            var users = await _userRepository.GetAllAsync();

            // Assert
            //Assert.Equal(2, users.);
            Assert.Contains(user1, users);
            Assert.Contains(user2, users);
        }
    }
}
