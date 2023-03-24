using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.ApplicationTest
{
    public class UserServiceTest
    {
        Mock<IUserRepository> _UserRepository = new Mock<IUserRepository>();
        UserService service;
        public UserServiceTest()
        {
            service = new UserService(_UserRepository.Object);
        }

        [Fact]
        public async Task CreateUserAsync()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Act
            User result = await service.CreateUser(NewUser());

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CreateSuperUserAsync()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Act
            User result = await service.CreateUser(NewSuperUser());

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CreatePremiumUserAsync()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Act
            User result = await service.CreateUser(NewPremiunUser());

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void ShouldThrowWhenCreateUser()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Assert
            Assert.Throws<AggregateException>(() => service.CreateUser(new User
            {
                Address = "Test",
                Money = 100,
                Phone = "300214541",
                UserId = 0,
                UserType = 1
            }).Result);
        }

        [Fact]
        public void ShouldThrowWhenMissingName()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Assert
            Assert.Throws<AggregateException>(() => service.CreateUser(new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 100,
                Phone = "300214541",
                UserId = 1,
                UserType = 1
            }).Result);
        }
        [Fact]
        public void ShouldThrowWhenMissingAddress()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Assert
            Assert.Throws<AggregateException>(() => service.CreateUser(new User
            {
                Email = "Test@test.com",
                Money = 100,
                Name = "Test",
                Phone = "300214541",
                UserId = 1,
                UserType = 1
            }).Result);
        }
        [Fact]
        public void ShouldThrowWhenMissingPhone()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Assert
            Assert.Throws<AggregateException>(() => service.CreateUser(new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 100,
                Name = "Test",
                UserId = 1,
                UserType = 1
            }).Result);
        }

        [Fact]
        public void ShouldThrowWhenDoNotEmail()
        {
            //Arrange
            _UserRepository.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(UserCreated());

            //Assert
            Assert.Throws<AggregateException>(() => service.CreateUser(new User
            {
                Address = "Test",
                Email = "Test",
                Money = 100,
                Name = "Test",
                UserId = 1,
                UserType = 1
            }).Result);
        }
        [Fact]
        public async Task GetAllUsersAsync()
        {
            //Arrange
            _UserRepository.Setup(x => x.GetAllUser()).ReturnsAsync(ListUser());

            //Act
            IEnumerable<User> result = await service.GetAllUser();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUserAsync()
        {  
            //Arrange
            _UserRepository.Setup(x => x.UpdateUser(It.IsAny<User>())).ReturnsAsync(UserCreated());
            //Act
            User result = await service.UpdateUser(UserCreated());

            //Assert
            Assert.NotNull(result);
        }


        private User UserCreated()
        {
            return new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 100,
                Name = "Test",
                Phone = "300214541",
                UserId = 1,
                UserType = 1
            };
        }

        private User NewUser()
        {
            return new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 100,
                Name = "Test",
                Phone = "300214541",
                UserId = 0,
                UserType = 1
            };
        }

        private User NewSuperUser()
        {
            return new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 150,
                Name = "Test",
                Phone = "300214541",
                UserId = 0,
                UserType = 2
            };
        }

        private User NewPremiunUser()
        {
            return new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 150,
                Name = "Test",
                Phone = "300214541",
                UserId = 0,
                UserType = 3
            };
        }

        private IEnumerable<User> ListUser()
        {
            List<User> UserList = new List<User>();
            UserList.Add(UserCreated());
            UserList.Add(UserCreated());
            return UserList;
        }
    }
}
