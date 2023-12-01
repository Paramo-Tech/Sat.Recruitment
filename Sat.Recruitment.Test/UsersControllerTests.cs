using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Controller;
using Sat.Recruitment.DTOs;
using Sat.Recruitment.Entities.Exceptions;
using Sat.Recruitment.Presenter;
using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{

    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        [Fact]
        public async Task CreateUser_WithValidUser_ReturnsOk()
        {
            // Arrange
            var mockUserInputPort = new Mock<IPostUserInputPort>();
            var mock = new Mock<IPostUserOutputPort>();
            mock.As<IPresenter<Result>>();
            mock.As<IPresenter<Result>>().SetupGet(p => p.Content).Returns(new Result { IsSuccess = true, Errors = "User Created" });


            var userDto = new UserDTO
            {
                Name = "test",
                Email = "test@test.com",
                Address = "Test 1",
                Phone = "1234567890",
                UserType = "Normal",
                Money = "20"

            };

            var controller = new UsersController(mockUserInputPort.Object, mock.Object);

            // Act
            var result = await controller.CreateUser(userDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_WithInvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var mockUserInputPort = new Mock<IPostUserInputPort>();
            var mockUserOutputPort = new Mock<IPostUserOutputPort>();

            var userDto = new UserDTO
            {
                Email = "test@test.com",
                Address = "Test 1",
                Phone = "1234567890",
                UserType = "Normal",
                Money = "test"
            };

            var controller = new UsersController(mockUserInputPort.Object, mockUserOutputPort.Object);

            // Act
            var result = await controller.CreateUser(userDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            var content = badRequestResult.Value as Result;

            Assert.False(content.IsSuccess);
            Assert.Equal("The name is required; Invalid number format", content.Errors);
        }

        [Fact]
        public async Task CreateUser_WithDuplicateUser_ReturnsConflict()
        {
            // Arrange
            var mockUserInputPort = new Mock<IPostUserInputPort>();

            var mock = new Mock<IPostUserOutputPort>();
            mock.As<IPresenter<Result>>();
            mock.As<IPresenter<Result>>().SetupGet(p => p.Content).Returns(new Result ());

            mockUserInputPort.Setup(p => p.Handle(It.IsAny<UserDTO>()))
                        .ThrowsAsync(new DuplicatedUserException("User already exists"));


            var userDto = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };

            var controller = new UsersController(mockUserInputPort.Object, mock.Object);

            // Act
            var result = await controller.CreateUser(userDto);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.False(((Result)conflictResult.Value).IsSuccess);
            Assert.Equal("The user is duplicated", ((Result)conflictResult.Value).Errors);
        }
    }

}
