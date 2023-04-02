using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test.Application
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IGiftFactory> _giftFactoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IGiftService> _giftService;
        private readonly Mock<ILogger<UserService>> _loggerMock;

        private readonly UserService _service;

        public UserServiceTest()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _giftFactoryMock = new Mock<IGiftFactory>();
            _mapperMock = new Mock<IMapper>();
            _giftService = new Mock<IGiftService>();
            _loggerMock = new Mock<ILogger<UserService>>();

            _service = new UserService(_repositoryMock.Object, _giftFactoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void GivenAWronUserCreationDtoWhenCreateUserThenReturnError()
        {
            //Arrange            
            var userCreatioDto = new UserCreationDto();

            //Act
            var result = await _service.Create(userCreatioDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Message);

            _giftFactoryMock.Verify(x => x.Create(It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(x => x.Map<User>(userCreatioDto), Times.Never);
            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Never);

        }

        [Fact]
        public async void GivenACorrectUserCreationDtoWhenCreateUserThenEmailIsDuplicated()
        {
            //Arrange
            var name = "Reinaldo";
            var email = "reinaldo.aospino@gmail.com";
            var address = "Belgrano";
            var phone = "112223488";
            var userType = "Normal";
            var money = "100";
            var userCreatioDto = GetUserCreationDto(name, email, address, phone, userType, money);
            var user = GetUser(name, email, address, phone, userType, money);

            var users = new List<User>
            {
                new User
                {
                    Name = "Jose",
                    Email = email,
                    Address = "some address",
                    Phone = "46546546",
                    UserType = userType,
                    Money = Convert.ToDecimal(money)
                }
            };

            _mapperMock.Setup(x => x.Map<User>(userCreatioDto)).Returns(user);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);


            //Act
            var result = await _service.Create(userCreatioDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Message);

            _giftFactoryMock.Verify(x => x.Create(It.IsAny<string>()), Times.Never);
            _mapperMock.VerifyAll();
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async void GivenACorrectUserCreationDtoWhenCreateUserThenPhoneIsDuplicated()
        {
            //Arrange
            var name = "Reinaldo";
            var email = "reinaldo.aospino@gmail.com";
            var address = "Belgrano";
            var phone = "112223488";
            var userType = "Normal";
            var money = "100";
            var userCreatioDto = GetUserCreationDto(name, email, address, phone, userType, money);
            var user = GetUser(name, email, address, phone, userType, money);

            var users = new List<User>
            {
                new User
                {
                    Name = "Jose",
                    Email = "some@email.com",
                    Address = "some address",
                    Phone = phone,
                    UserType = userType,
                    Money = Convert.ToDecimal(money)
                }
            };

            _mapperMock.Setup(x => x.Map<User>(userCreatioDto)).Returns(user);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);


            //Act
            var result = await _service.Create(userCreatioDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Message);

            _giftFactoryMock.Verify(x => x.Create(It.IsAny<string>()), Times.Never);
            _mapperMock.VerifyAll();
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async void GivenACorrectUserCreationDtoWhenCreateUserThenNameAndAdressAreDuplicated()
        {
            //Arrange
            var name = "Reinaldo";
            var email = "reinaldo.aospino@gmail.com";
            var address = "Belgrano";
            var phone = "112223488";
            var userType = "Normal";
            var money = "100";
            var userCreatioDto = GetUserCreationDto(name, email, address, phone, userType, money);
            var user = GetUser(name, email, address, phone, userType, money);

            var users = new List<User>
            {
                new User
                {
                    Name = name,
                    Email = "some@email.com",
                    Address = address,
                    Phone = "1234",
                    UserType = userType,
                    Money = Convert.ToDecimal(money)
                }
            };

            _mapperMock.Setup(x => x.Map<User>(userCreatioDto)).Returns(user);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);


            //Act
            var result = await _service.Create(userCreatioDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Message);

            _giftFactoryMock.Verify(x => x.Create(It.IsAny<string>()),Times.Never);
            _mapperMock.VerifyAll();
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async void GivenACorrectUserCreationDtoWhenCreateUserThenUserWasCreated()
        {
            //Arrange
            var name = "Reinaldo";
            var email = "reinaldo.aospino@gmail.com";
            var address = "Belgrano";
            var phone = "112223488";
            var userType = "Normal";
            var money = "100";
            var userCreatioDto = GetUserCreationDto(name, email, address, phone, userType, money);
            var user = GetUser(name, email, address, phone, userType, money);

            var users = new List<User>
            {
                new User
                {
                    Name = "Jose",
                    Email = "some@email.com",
                    Address = "some andress",
                    Phone = "1234",
                    UserType = userType,
                    Money = Convert.ToDecimal(money)
                }
            };

            _mapperMock.Setup(x => x.Map<User>(userCreatioDto)).Returns(user);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
            _giftFactoryMock.Setup(x=>x.Create(userType)).Returns(_giftService.Object);
            _repositoryMock.Setup(x=>x.CreateAsync(user));


            //Act
            var result = await _service.Create(userCreatioDto);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Message);
            Assert.Null(result.Errors);

            _giftFactoryMock.VerifyAll();
            _mapperMock.VerifyAll();
            _repositoryMock.VerifyAll();
        }

        static User GetUser(string name, string email, string address, string phone, string userType, string money)
        {
            return new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = Convert.ToDecimal(money)
            };
        }

        static UserCreationDto GetUserCreationDto(string name, string email, string address, string phone, string userType, string money)
        {
            return new UserCreationDto
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };
        }
    }
}
