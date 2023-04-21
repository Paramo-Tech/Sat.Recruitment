using Sat.Recruitment.Api.Services;
using Moq;
using System;
using Xunit;
using Sat.Recruitment.Api.Models.DTOs;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api;

namespace Sat.Recruitment.Test
{
    public class UserTests
    {
        [Fact]
        public void UserService_Should_Return_NewUser()
        {
            // Arrange
            var userModelDto = new UserModelDto
            {
                Name = "Test",  Phone = "+5196258999", Address = "Lima, PERU", Email = "test@email.com", Money = Convert.ToDecimal(101.90),
                UserTypeId = Constants.UserTypesId.SuperUser
            };

            var _userTypeRepositoryMock = new Mock<IUserTypeRepository>();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.CreateUpdate(userModelDto))
                .ReturnsAsync(userModelDto);

            var service = new UserService(userRepositoryMock.Object, _userTypeRepositoryMock.Object);

            // Act
            var user = service.CreateUpdateUser(userModelDto);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(user.Result.Name, userModelDto.Name);
        }

        [Fact]
        public void UserService_Should_Return_Duplicate_True()
        {
            // Arrange
            var userModelDto = new UserModelDto
            {
                Name = "Test",
                Phone = "+5196258999",
                Address = "Lima, PERU",
                Email = "test@email.com",
                Money = Convert.ToDecimal(101.90),
                UserTypeId = Constants.UserTypesId.Normal
            };

            var _userTypeRepositoryMock = new Mock<IUserTypeRepository>();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.FindDuplicate(userModelDto))
                .ReturnsAsync(true);

            var service = new UserService(userRepositoryMock.Object, _userTypeRepositoryMock.Object);

            // Act
            var user = service.IsUserDuplicate(userModelDto);

            // Assert
            Assert.True(user.Result);
        }

        [Fact]
        public void UserService_Should_Return_Duplicate_False()
        {
            // Arrange
            var userModelDto = new UserModelDto
            {
                Name = "Test",
                Phone = "+5196258999",
                Address = "Lima, PERU",
                Email = "test@email.com",
                Money = Convert.ToDecimal(101.90),
                UserTypeId = Constants.UserTypesId.SuperUser
            };

            var _userTypeRepositoryMock = new Mock<IUserTypeRepository>();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.FindDuplicate(userModelDto))
                .ReturnsAsync(false);

            var service = new UserService(userRepositoryMock.Object, _userTypeRepositoryMock.Object);

            // Act
            var user = service.IsUserDuplicate(userModelDto);

            // Assert
            Assert.False(user.Result);
        }

        [Fact]
        public void UserService_Should_Return_CorrectMoney()
        {
            // Arrange
            var userModelDto = new UserModelDto
            {
                Name = "Test",
                Phone = "+5196258999",
                Address = "Lima, PERU",
                Email = "test@email.com",
                Money = Convert.ToDecimal(101),
                UserTypeId = Constants.UserTypesId.SuperUser
            };

            var _userTypeRepositoryMock = new Mock<IUserTypeRepository>();

            _userTypeRepositoryMock.Setup(x => x.GetTypeById(userModelDto.UserTypeId))
            .ReturnsAsync(Constants.UserTypes.Premium);

            var userRepositoryMock = new Mock<IUserRepository>();

            var service = new UserService(userRepositoryMock.Object, _userTypeRepositoryMock.Object);

            // Act
            var money = service.SetMoney(Convert.ToDecimal(userModelDto.Money), Constants.UserTypes.Premium);

            // Assert
            Assert.Equal(Convert.ToDecimal(303), money);
        }

        [Fact]
        public void UserService_Should_Return_IncorrectMoney()
        {
            // Arrange
            var userModelDto = new UserModelDto
            {
                Name = "Test",
                Phone = "+5196258999",
                Address = "Lima, PERU",
                Email = "test@email.com",
                Money = Convert.ToDecimal(101),
                UserTypeId = Constants.UserTypesId.SuperUser
            };

            var _userTypeRepositoryMock = new Mock<IUserTypeRepository>();

            _userTypeRepositoryMock.Setup(x => x.GetTypeById(userModelDto.UserTypeId))
            .ReturnsAsync(Constants.UserTypes.Normal);

            var userRepositoryMock = new Mock<IUserRepository>();

            var service = new UserService(userRepositoryMock.Object, _userTypeRepositoryMock.Object);

            // Act
            var money = service.SetMoney(Convert.ToDecimal(userModelDto.Money), Constants.UserTypes.Premium);

            // Assert
            Assert.NotEqual(Convert.ToDecimal(10), money);
        }
    }
}
