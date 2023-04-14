using System;
using System.Dynamic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Contracts.Application;
using Sat.Recruitment.Application.Dto;
using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Domain.Entities;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IMapper> _mapper;
        public UsersControllerTests()
        {
            this._userService = new Mock<IUserService>();
            this._mapper = new Mock<IMapper>();

        }

        [Fact]
        public void Create_OnSuccess_User()
        {
            // arrange
            UserDto userDto = GetUserDto();
            User user = null;
            string message = "User Created";
            _mapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            this._userService
            .Setup(x => x.CreateUser(user))
            .Returns(new ResultUser { IsSuccess = true, Message = message });
            UsersController usersController = new UsersController(_userService.Object, _mapper.Object);

            // act
            var result = usersController.CreateUser(userDto);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void Create_OnFailure_User()
        {
            // arrange
            UserDto userDto = GetUserDto();
            User user = null;
            string message = "The user is duplicated";
            _mapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            this._userService
            .Setup(x => x.CreateUser(user))
            .Returns(new ResultUser { IsSuccess = false, Message = message });
            UsersController usersController = new UsersController(_userService.Object, _mapper.Object);

            // act
            var result = usersController.CreateUser(userDto);

            // assert
            Assert.True(!result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
        [Fact]
        public void UserController_Constructor_Null()
        {
            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(null, null));
        }
        private UserDto GetUserDto()
        {
            // arrange
            return new UserDto()
            {

                Name = "Test",
                Email = "test@test.com",
                Address = "testAddress",
                Phone = "+57123456",
                UserType = "Normal",
                Money = 100
            };
        }
    }
}
