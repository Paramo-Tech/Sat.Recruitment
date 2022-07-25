using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [Collection("Users")]
    public class UserControllerTest
    {
        private UserModel userModel;
        private readonly List<User> listUsers;

        public UserControllerTest()
        {
            #region Global Pre-Arrange
            listUsers = new List<User>
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Andres",
                    Address = "Juan B Justo",
                    Email = "andresmiretti@gmail.com",
                    Money = 1233445,
                    Phone = "+54923889920",
                    UserType = Models.Enums.UserType.SuperUser
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Benicio",
                    Address = "Juan B Justo",
                    Email = "beniciomiretti@gmail.com",
                    Money = 123344225,
                    Phone = "+54923889000",
                    UserType = Models.Enums.UserType.Premium
                }
            };

            userModel = new UserModel()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = Models.Enums.UserType.Normal,
                Money = 124
            }; 
            #endregion
        }

        [Fact(DisplayName = "Post with complete and correct info, must return success")]
        public async void Post_WhenCompleteAndCorrectInfo_MustReturnSuccess()
        {
            //Arrange
            var response = new Result() { 
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));
            userSvcMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.True(validationResults.Count == 0);
        }

        [Fact(DisplayName = "Post with correct info, but without Email must return a validation error")]
        public async void Post_WhenCorrectInfoButWithoutEmail_MustReturnValidationError()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));

            userModel.Email = null;

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.True(validationResults.Count > 0);
            Assert.True(validationResults.Where(x => x.ErrorMessage == "The email is required").Any());
        }

        [Fact(DisplayName = "Post with complete info, but incorrect Email must return a validation error")]
        public async void Post_WhenCompleteInfoButIncorrectEmail_MustReturnValidationError()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));

            userModel.Email = "email_without_at_symbol";

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.True(validationResults.Count > 0);
            Assert.True(validationResults.Where(x => x.ErrorMessage == "The Email field is not a valid e-mail address.").Any());
        }

        [Fact(DisplayName = "Post with correct info, but without phone must return a validation error")]
        public async void Post_WhenCorrectInfoButWithoutPhone_MustReturnValidationError()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));

            userModel.Phone = null;

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.True(validationResults.Count > 0);
            Assert.True(validationResults.Where(x => x.ErrorMessage == "The phone is required").Any());
        }

        [Fact(DisplayName = "Post with complete info, but incorrect Phone must return a validation error")]
        public async void Post_WhenCompleteInfoButIncorrectPhone_MustReturnValidationError()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));

            userModel.Phone = "phone_without_correct_format";

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.True(validationResults.Count > 0);
            Assert.True(validationResults.Where(x => x.ErrorMessage == "The Phone field is not a valid phone number.").Any());
        }

        [Fact(DisplayName = "Post with correct info, but without Name must return a validation error")]
        public async void Post_WhenCorrectInfoButWithoutName_MustReturnValidationError()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));

            userModel.Name = null;

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.True(validationResults.Count > 0);
            Assert.True(validationResults.Where(x => x.ErrorMessage == "The name is required").Any());
        }

        [Fact(DisplayName = "Post with correct info, but without Address must return a validation error")]
        public async void Post_WhenCorrectInfoButWithoutAddress_MustReturnValidationError()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };

            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Save(It.IsAny<UserModel>())).Returns(Task.Run(() => response));

            userModel.Address = null;

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Post(userModel);

            //Assert
            Assert.True(validationResults.Count > 0);
            Assert.True(validationResults.Where(x => x.ErrorMessage == "The address is required").Any());
        }

        [Fact(DisplayName = "Get REST method must return a list of users")]
        public async void Get_WhenCallTheRestMethodGet_MustReturnListOfUsers()
        {
            //Arrange
            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));

            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(userModel, null, null);
            Validator.TryValidateObject(userModel, ctx, validationResults, true);

            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Get();

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.True(validationResults.Count == 0);
        }

        [Fact(DisplayName = "Get by Id when we request for an existing user, must return a Ok response")]
        public async void GetById_WhenRequestForAnExistingUser_MustReturnAOk()
        {
            //Arrange
            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Get(It.IsAny<Guid>()))
                .Returns<Guid>(id => Task.Run(() => listUsers.SingleOrDefault(usr => usr.Id == id)));
            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Get(listUsers.First().Id);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Get by Id when we request for a existing user, must return a Not Found response")]
        public async void GetById_WhenRequestForANoExistingUser_MustReturnANotFound()
        {
            //Arrange
            var userSvcMock = new Mock<IUserSvc>();
            userSvcMock.Setup(u => u.Get(It.IsAny<Guid>()))
               .Returns<Guid>(id => Task.Run(() => listUsers.SingleOrDefault(usr => usr.Id == id)));
            UsersController controller = new UsersController(userSvcMock.Object);

            //Act
            var result = await controller.Get(Guid.NewGuid());

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
    }
}
