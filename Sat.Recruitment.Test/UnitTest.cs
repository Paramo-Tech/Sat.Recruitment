using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.BusinessLogic;
using Sat.Recruitment.Application.Interfaces.Repositories;
using Sat.Recruitment.Application.Interfaces.Services;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Infrastructure.Services;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest
    {
        Mock<IUserBL> _userBL;
        Mock<IUserRepository> _userRepository;
        Mock<IMapper> _mapper;
        Mock<UserService> _mockIUserService;
        public UnitTest()
        {
            _userBL = new Mock<IUserBL>();
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.GetAll()).Returns(GetUsers());
            _mapper = new Mock<IMapper>();
            _mockIUserService = new Mock<UserService>(_userBL.Object, _userRepository.Object, _mapper.Object);
        }
        private List<User> GetUsers()
        {
            var _users = new List<User>();
            _users.Add(new User() { Name = "Jorge", Email = "Jorge22@gmail.com", Phone = "+5491121231234", Address = "Av Garay", Money = 100 });
            _users.Add(new User() { Name = "Agustina", Email = "Agustina@gmail.com", Phone = "3491122354215", Address = "Av. Juan G", Money = 1000 });

            return _users;
        }
        [Fact]
        public void Add_NewUser_ReturnsCreatedCode()
        {
            Mock<IUserService> mockIUserService = new Mock<IUserService>();

            var userController = new UsersController(mockIUserService.Object);

            var result = userController.CreateUser(new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "3491122354215",
                UserType = "Normal",
                Money = 124
            }) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void Add_RepeatedUser_ReturnsDuplicatedEmail()
        {

            var userController = new UsersController(_mockIUserService.Object);

            var result = userController.CreateUser(new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "3491122354215",
                UserType = "Normal",
                Money = 124
            }) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Duplicated User Email", result.Value);
        }

        [Fact]
        public void Add_RepeatedUser_ReturnsDuplicatedPhone()
        {

            var userController = new UsersController(_mockIUserService.Object);

            var result = userController.CreateUser(new User()
            {
                Name = "Agustina",
                Email = "testEmailOk@gmail.com",
                Address = "Av. Juan G",
                Phone = "3491122354215",
                UserType = "Normal",
                Money = 124
            }) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Duplicated User Phone", result.Value);
        }
        [Fact]
        public void Add_RepeatedUser_ReturnsDuplicatedNameAndAddress()
        {

            var userController = new UsersController(_mockIUserService.Object);

            var result = userController.CreateUser(new User()
            {
                Name = "Agustina",
                Email = "testEmailOk@gmail.com",
                Address = "Av. Juan G",
                Phone = "3491",
                UserType = "Normal",
                Money = 124
            }) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Duplicated User Name and Address", result.Value);
        }
    }
}
