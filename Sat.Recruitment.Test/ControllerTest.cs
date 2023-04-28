using System;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Responses;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Responses;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;

        public UnitTest1()
        {
            _loggerMock = new Mock<ILogger<UsersController>>();
            _userServiceMock = new Mock<IUserService>();

            var mapperConf = new MapperConfiguration(cfg => cfg.AddProfile(new Api.Mappings.AutoMapperProfiles()));
            _mapper = mapperConf.CreateMapper();
        }

        [Fact]
        public async Task CreateUser_ReturnBadRequest()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock.Object, _loggerMock.Object, _mapper);
            var invalidUserDto = new UserDTO { Name = "Ricardo", Email = "ricardo@gmail.com", Address = "", Phone = "", UserType = "Normal", Money = 500 };

            // Act
            var result = await usersController.CreateUser(invalidUserDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<Response>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Message);
        }

        [Fact]
        public async Task CreateUser_ReturnBadRequestUserDuplicated()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock.Object, _loggerMock.Object, _mapper);
            var validUserDto = new UserDTO { Name = "Ricardo", Email = "ricardo@gmail.com", Address = "chan 12", Phone = "123-456-7890", UserType = "Normal", Money = 500 };

            _userServiceMock.Setup(us => us.Create(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await usersController.CreateUser(validUserDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<Response>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("The user is duplicated", response.Message);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedSuccessfully()
        {
            // Arrange
            var usersController = new UsersController(_userServiceMock.Object, _loggerMock.Object, _mapper);
            var validUserDto = new UserDTO { Name = "Clark Kent", Email = "clark.kent@dailyplanet.com", Address = "344 Clinton St", Phone = "555-123-4567", UserType = "SuperUser", Money = 1000};

            _userServiceMock.Setup(us => us.Create(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await usersController.CreateUser(validUserDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var response = Assert.IsType<Response>(createdAtActionResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("User Created", response.Message);
        }
    }
}
