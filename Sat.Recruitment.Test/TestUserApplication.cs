using Application.Interfaces;
using Application.UseCases.user;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Infraestructure.Configdb;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class TestUserApplication
    {
        [Fact]
        public async void TestCreateNormalUserAsync()
        {

            //Given
            IUserType normalUser = new UserDomain()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = Enum.Parse<UserType>("Normal"),
                Money = 2000
            };


            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByName(normalUser.Name, normalUser.Email)).ReturnsAsync(value: null);
            userRepository.Setup(x => x.Create(normalUser)).ReturnsAsync(true);
            User user = new User(userRepository.Object);


            //When
            var resultInsert = await user.CreateUser(normalUser);

            //Then
            Assert.True(resultInsert);
            userRepository.Verify(x => x.Create(normalUser));
        }
        [Fact]
        public async void TestCreateFailNormalUserAsync()
        {

            //Given
            IUserType normalUser = new UserDomain()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = Enum.Parse<UserType>("Normal"),
                Money = 2000
            };


            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.Create(normalUser)).ReturnsAsync(false);
            User user = new User(userRepository.Object);


            //When
            var resultInsert = await user.CreateUser(normalUser);

            //Then
            Assert.False(resultInsert);
            userRepository.Verify(x => x.Create(normalUser));
        }

        [Fact]
        public async Task ExceptionTestGetUser()
        {

            //Given
            IUserType normalUser = new UserDomain()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = Enum.Parse<UserType>("Normal"),
                Money = 2000
            };

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByName(normalUser.Name, normalUser.Email)).ReturnsAsync(normalUser);

            User user = new User(userRepository.Object);

            //When
            var ex = await Assert.ThrowsAsync<ApplicationException>(() => user.CreateUser(normalUser));

            //Then
            Assert.Equal("user exists", ex.Message);
            userRepository.Verify(x => x.GetUserByName(normalUser.Name, normalUser.Email));
        }

        [Fact]
        public async Task TestGetUserById()
        {
            //Given
            IUserType normalUser = new UserDomain()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = Enum.Parse<UserType>("Normal"),
                Money = 2000
            };

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserById(1)).ReturnsAsync(normalUser);

            User user = new User(userRepository.Object);

            //When
            var resultGet = await user.GetUser(1);

            //Then 
            Assert.Equal(normalUser, resultGet);

        }

        [Fact]
        public async Task ExceptionTestGetUserById()
        {
            //Given
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserById(1)).ReturnsAsync(value: null);

            User user = new User(userRepository.Object);

            //When
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => user.GetUser(1));

            //Then
            Assert.Equal("user not found", ex.Message);
            userRepository.Verify(x => x.GetUserById(1));
        }

        [Fact]
        public void ExceptionTestValidateEmail()
        {
            //Given
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();


            User user = new User(userRepository.Object);

            //When
            var ex =  Assert.Throws<NullReferenceException>(() => user.ValidateEmail(null));

            //Then
            Assert.Equal("email Null", ex.Message);

        }

    }
}
