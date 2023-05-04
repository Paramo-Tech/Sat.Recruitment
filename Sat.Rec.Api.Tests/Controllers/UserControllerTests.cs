using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Rec.Api.Controllers;
using Sat.Rec.Api.DTO;
using Sat.Rec.Core.Services.Interfaces;
using Sat.Rec.Core.Validation;
using Sat.Rec.Models;

namespace Sat.Rec.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        #region GetAll

        [TestMethod]
        public void When_UserService_GetAll_Returns_OK_Controller_GetUserList_Returns_OKAndList()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = 200,
                Errors = new List<string>(),
                ResultList = usersToRetrieve.ToList(),
                SingleResult = null
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetAll()).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.GetUsersList().Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            OkObjectResult okObjectResult = (OkObjectResult)result;
            Assert.IsNotNull(okObjectResult.Value);

            List<User> okObjectResultValue = (List<User>)(okObjectResult.Value ?? new List<User>());
            Assert.AreEqual(usersToRetrieve.Count(), okObjectResultValue.Count);
        }

        [TestMethod]
        public void When_UserService_GetAll_Returns_Errors_Controller_GetUserList_Returns_InternalServerError()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = int.MaxValue, //It doesn't matter
                Errors = new List<string>() { "Some random error" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetAll()).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.GetUsersList().Result;
            var objectResult = (ObjectResult)result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        #endregion

        #region GetById
        [TestMethod]
        public void When_UserService_GetById_Returns_OK_Controller_GetUserList_Returns_OKAndSingleResult()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var existingUserId = usersToRetrieve.First().UserId;

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = 200,
                Errors = new List<string>(),
                ResultList = new List<User>(),
                SingleResult = usersToRetrieve.First()
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetById(existingUserId)).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.GetUserById(existingUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            OkObjectResult okObjectResult = (OkObjectResult)result;
            Assert.IsNotNull(okObjectResult.Value);

            User okObjectResultValue = (User)okObjectResult.Value;
            Assert.AreEqual(usersToRetrieve.First().UserId, okObjectResultValue.UserId);
            Assert.AreEqual(usersToRetrieve.First().Email, okObjectResultValue.Email);
            Assert.AreEqual(usersToRetrieve.First().Name, okObjectResultValue.Name);
            Assert.AreEqual(usersToRetrieve.First().Phone, okObjectResultValue.Phone);
            Assert.AreEqual(usersToRetrieve.First().UserTypeId, okObjectResultValue.UserTypeId);
            Assert.AreEqual(usersToRetrieve.First().Money, okObjectResultValue.Money);
        }

        [TestMethod]
        public void When_UserService_GetById_Returns_Errors_Controller_GetUserList_Returns_InternalServerError()
        {
            //Arrange

            var someRandomId = new Random().Next(100, 10000);

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = int.MaxValue,                        //It doesn't matter
                Errors = new List<string>() { "Some random error" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetById(someRandomId)).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.GetUserById(someRandomId).Result;
            var objectResult = (ObjectResult)result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [TestMethod]
        public void When_UserService_GetById_Returns_Errors_And_BadRequest_Controller_GetUserList_Returns_InternalServerError()
        {
            //Arrange

            var someRandomId = new Random().Next(100, 10000);

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = (int)HttpStatusCode.BadRequest,
                Errors = new List<string>() { "Some random error" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetById(someRandomId)).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.GetUserById(someRandomId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void When_UserService_GetById_Returns_NotFound_Controller_GetUserList_Returns_NotFound()
        {
            //Arrange

            var someRandomId = int.MaxValue;                            //Some Id that is not on the current mocked list

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = StatusCodes.Status404NotFound,
                Errors = new List<string>(),
                ResultList = new List<User>(),
                SingleResult = null
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetById(someRandomId)).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.GetUserById(someRandomId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        #endregion

        #region CreateUser
        [TestMethod]
        public void When_UserService_CreateUser_Returns_Created_Controller_CreateUser_Returns_Created()
        {
            //Arrange

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = StatusCodes.Status201Created,
                Errors = new List<string>(),
                ResultList = new List<User>(),
                SingleResult = null
            };

            var newUserDTO = new CreateUserDTO
            {
                Address = "Some Address 3",
                Email = "first.name+secondlastname@domain.com",
                Money = 100,
                Name = "Test 3",
                Phone = "156789",
                UserTypeId = 2
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.CreateUser(newUserDTO).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public void When_UserService_CreateUser_Returns_BadRequest_Controller_CreateUser_Returns_BadRequest()
        {
            //Arrange

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = StatusCodes.Status400BadRequest,
                Errors = new List<string>() { "Some Error text" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var newUserDTO = new CreateUserDTO
            {
                Address = "Some Address 3",
                Email = "first.name+secondlastname@domain.com",
                Money = 100,
                Name = "Test 3",
                Phone = "156789",
                UserTypeId = 2
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.CreateUser(newUserDTO).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void When_UserService_CreateUser_Returns_InternalServerError_Controller_CreateUser_Returns_InternalServerError()
        {
            //Arrange

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = StatusCodes.Status500InternalServerError,
                Errors = new List<string>() { "Some Error text" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var newUserDTO = new CreateUserDTO
            {
                Address = "Some Address 3",
                Email = "first.name+secondlastname@domain.com",
                Money = 100,
                Name = "Test 3",
                Phone = "156789",
                UserTypeId = 2
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.CreateUser(newUserDTO).Result;
            var objectResult = (ObjectResult)result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateUser
        [TestMethod]
        public void When_UserService_UpdateUser_Returns_NoErrors_Controller_UpdateUser_Returns_NoContent()
        {
            //Arrange

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = int.MaxValue,                    //It does not matter
                Errors = new List<string>(),
                ResultList = new List<User>(),
                SingleResult = null
            };
            var randomUserId = new Random().Next(100, 1000);
            var newUserDTO = new UpdateUserDTO
            {
                Address = "Some Address 3",
                Email = "first.name+secondlastname@domain.com",
                Money = 100,
                Name = "Test 3",
                Phone = "156789",
                UserTypeId = 2
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Update(It.IsAny<User>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.UpdateUser(randomUserId, newUserDTO).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void When_UserService_UpdateUser_Returns_BadRequest_Controller_UpdateUser_Returns_BadRequest()
        {
            //Arrange

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = StatusCodes.Status400BadRequest,
                Errors = new List<string>() { "Some Error text" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var randomUserId = new Random().Next(100, 1000);
            var newUserDTO = new UpdateUserDTO
            {
                Address = "Some Address 3",
                Email = "first.name+secondlastname@domain.com",
                Money = 100,
                Name = "Test 3",
                Phone = "156789",
                UserTypeId = 2
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Update(It.IsAny<User>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.UpdateUser(randomUserId, newUserDTO).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void When_UserService_UpdateUser_Returns_InternalServerError_Controller_UpdateUser_Returns_InternalServerError()
        {
            //Arrange

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = StatusCodes.Status500InternalServerError,
                Errors = new List<string>() { "Some Error text" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var randomUserId = new Random().Next(100, 1000);
            var newUserDTO = new UpdateUserDTO
            {
                Address = "Some Address 3",
                Email = "first.name+secondlastname@domain.com",
                Money = 100,
                Name = "Test 3",
                Phone = "156789",
                UserTypeId = 2
            };

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Update(It.IsAny<User>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.UpdateUser(randomUserId, newUserDTO).Result;
            var objectResult = (ObjectResult)result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region DeleteUser
        [TestMethod]
        public void When_UserService_DeleteUser_Returns_NoErrors_Controller_DeleteUser_Returns_NoContent()
        {
            //Arrange
            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = int.MaxValue,                    //Dummy value
                Errors = new List<string>(),
                ResultList = new List<User>(),
                SingleResult = null
            };

            var randomUserId = new Random().Next(100, 1000);

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.DeleteUser(randomUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void When_UserService_DeleteUser_Returns_UnspecifiedErrors_Controller_DeleteUser_Returns_InternalServerError()
        {
            //Arrange
            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = int.MaxValue,                    //Dummy value
                Errors = new List<string>() { "Some Error text" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var randomUserId = new Random().Next(100, 1000);

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.DeleteUser(randomUserId).Result;
            var objectResult = (ObjectResult)result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [TestMethod]
        public void When_UserService_DeleteUser_Returns_BadRequestErrors_Controller_DeleteUser_Returns_BadRequest()
        {
            //Arrange
            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = (int)HttpStatusCode.BadRequest,                    //Dummy value
                Errors = new List<string>() { "Some Error text" },
                ResultList = new List<User>(),
                SingleResult = null
            };

            var randomUserId = new Random().Next(100, 1000);

            var loggerMock = new Mock<ILogger<UserController>>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(validationResult));

            //ACT
            var controllerActionCall = new UserController(loggerMock.Object, userServiceMock.Object);
            var result = controllerActionCall.DeleteUser(randomUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        #endregion

        private List<User> GetRegularListOfUser()
        {
            return new List<User>()
            {
                new User{
                Address = "Some Address",
                Email = "Some Email",
                Money = 15.2M,
                Name = "Test",
                Phone = "",
                UserId = 156,
                    UserTypeId = 1
                },
                new User{
                Address = "Some Address 2 ",
                Email = "Some Email 2 ",
                Money = 99.2M,
                Name = "Test 2",
                Phone = "",
                UserId = 465,
                    UserTypeId =2
                }
            };
        }
    }
}