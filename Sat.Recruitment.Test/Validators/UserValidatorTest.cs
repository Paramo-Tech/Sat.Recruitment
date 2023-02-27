using System;
using System.Linq;
using Xunit;
using FluentValidation.TestHelper;
using Sat.Recruitment.Api.Models.Validators;
using Moq;
using Sat.Recruitment.Api.Repository.Interfaces;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Test.Validators
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserValidatorTest
    {
        private Mock<IUserRepository> userRepositoryMock;

        private UserValidator validator;
        public UserValidatorTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            validator = new UserValidator(userRepositoryMock.Object);
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            var model = new UserDTO
            {
                Name = null,
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(false);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(User => User.Name).Only();
        }

        [Fact]
        public void Should_have_error_when_Phone_is_null()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = null,
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(false);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(User => User.Phone).Only();
        }

        [Fact]
        public void Should_have_error_when_Email_is_null()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = null,
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(false);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(User => User.Email).Only();
        }

        [Fact]
        public void Should_have_error_when_Address_is_null()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = null,
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(false);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(User => User.Address).Only();
        }

        [Fact]
        public void Should_have_error_when_Email_IsDuplicated()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(true);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Email);

            Assert.Equal("User is duplicated", result.Errors.First().ErrorMessage);
        }

        [Fact]
        public void Email_Should_have_been_normalized()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina.gomez+st1234@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(false);
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();

            Assert.Equal("Agustinagomez@gmail.com", model.Email);
        }

        [Fact]
        public void Should_have_error_when_Name_and_Address_AreDuplicated()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(true);
            var result = validator.TestValidate(model);

            Assert.Equal("User is duplicated", result.Errors.First().ErrorMessage); 
        }

        [Fact]
        public void Should_have_no_error()
        {
            var model = new UserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypes.NORMAL,
                Money = 124
            };
            userRepositoryMock.Setup(x => x.Any(It.IsAny<Func<IUser, bool>>())).Returns(false);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
