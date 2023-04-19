using AutoMapper;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Core.DTOs;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserService> _userServiceMock;

        public UsersControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _userServiceMock = new Mock<IUserService>();
            _controller = new UsersController(_mapperMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnUserCreated_WhenUserIsNotDuplicated()
        {
            // Arrange
            var userDto = new UserDto ("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);
            var userEntity = new USER("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);

            _mapperMock.Setup(m => m.Map<USER>(userDto)).Returns(userEntity);
            _userServiceMock.Setup(u => u.Create(userEntity)).ReturnsAsync("User Created");

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnUserDuplicated_WhenUserIsDuplicated()
        {
            // Arrange
            var userDto = new UserDto ("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);
            var userEntity = new USER ("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);

            _mapperMock.Setup(m => m.Map<USER>(userDto)).Returns(userEntity);
            _userServiceMock.Setup(u => u.Create(userEntity)).ReturnsAsync("The User is duplicated");

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The User is duplicated", result.Message);
        }
    }
}
