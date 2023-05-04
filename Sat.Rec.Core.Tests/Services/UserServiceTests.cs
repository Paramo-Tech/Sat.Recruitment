using System.ComponentModel.DataAnnotations;
using System.Net;
using Moq;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Core.Services;
using Sat.Rec.Core.Validation;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        #region GetAll

        [TestMethod]
        public void When_Repository_GetAll_Returns_OK_Service_Update_Returns_OKAndList()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();

            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = (int)HttpStatusCode.OK,
                Errors = new List<string>(),
                ResultList = usersToRetrieve.ToList(),
                SingleResult = null
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetAll()).Returns(Task.FromResult(usersToRetrieve.AsEnumerable()));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.GetAll().Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.OK, result.CustomResultCode);
            Assert.AreEqual(usersToRetrieve.Count, result.ResultList.Count);
            Assert.AreEqual(usersToRetrieve.First().UserId, result.ResultList.First().UserId);
            Assert.AreEqual(usersToRetrieve.First().Address, result.ResultList.First().Address);
            Assert.AreEqual(usersToRetrieve.First().Email, result.ResultList.First().Email);
            Assert.AreEqual(usersToRetrieve.First().Phone, result.ResultList.First().Phone);
            Assert.AreEqual(usersToRetrieve.First().Name, result.ResultList.First().Name);
            Assert.AreEqual(usersToRetrieve.First().Money, result.ResultList.First().Money);
            Assert.AreEqual(usersToRetrieve.First().UserTypeId, result.ResultList.First().UserTypeId);
        }

        [TestMethod]
        public void When_Repository_GetAll_ThrowsEx_Service_Update_Returns_OKAndList()
        {
            //Arrange

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetAll()).Throws(new Exception());

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.GetAll().Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.CustomResultCode);
        }

        #endregion

        #region GetById
        [TestMethod]
        public void When_Repository_GetById_Returns_OK_Controller_GetUserList_Returns_OKAndSingleResult()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var randomUserId = new Random().Next(100, 1000);
            var validationResult = new ValidationResult<User>()
            {
                CustomResultCode = (int)HttpStatusCode.OK,
                Errors = new List<string>(),
                ResultList = usersToRetrieve.ToList(),
                SingleResult = null
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Returns(Task.FromResult(usersToRetrieve.First()));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.GetById(randomUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.OK, result.CustomResultCode);
            Assert.IsNotNull(result.SingleResult);
            Assert.AreEqual(usersToRetrieve.First().UserId, result.SingleResult.UserId);
            Assert.AreEqual(usersToRetrieve.First().Address, result.SingleResult.Address);
            Assert.AreEqual(usersToRetrieve.First().Email, result.SingleResult.Email);
            Assert.AreEqual(usersToRetrieve.First().Phone, result.SingleResult.Phone);
            Assert.AreEqual(usersToRetrieve.First().Name, result.SingleResult.Name);
            Assert.AreEqual(usersToRetrieve.First().Money, result.SingleResult.Money);
            Assert.AreEqual(usersToRetrieve.First().UserTypeId, result.SingleResult.UserTypeId);
        }

        [TestMethod]
        public void When_Repository_GetById_Returns_ErrorsAndBadRequest_Controller_GetUserList_Returns_BadRequest()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var lessThanOneUserId = 0;

            User nullUser = null;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Returns(Task.FromResult(nullUser));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.GetById(lessThanOneUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.CustomResultCode);
        }

        [TestMethod]
        public void When_Repository_GetById_Returns_Null_Controller_GetUserList_Returns_NotFound()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var someRandomValidUserId = new Random().Next(100, 1000);

            User nullUser = null;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Returns(Task.FromResult(nullUser));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.GetById(someRandomValidUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.CustomResultCode);
        }

        [TestMethod]
        public void When_Repository_GetById_ThrowsException_Controller_GetUserList_Returns_InternalServerError()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var someRandomValidUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Throws(new Exception());

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.GetById(someRandomValidUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.CustomResultCode);
        }
        #endregion

        #region Update
        [TestMethod]
        public void When_Repository_Update_Returns_Null_Service_Update_Returns_BadRequest()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var userToUpdate = usersToRetrieve.First();
            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);
            var randomUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Update(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToUpdate.UserTypeId)).Returns(Task.FromResult(userType));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Update(usersToRetrieve.First()).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.CustomResultCode);
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_Repository_Update_Returns_Errors_Service_Update_Returns_BadRequest()
        {
            //Arrange

            var userList = GetRegularListOfUser();

            var userToUpdate = userList.First();
            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Update(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToUpdate.UserTypeId)).Returns(Task.FromResult(userType));
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult(userList.Last()));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Update(userToUpdate).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.CustomResultCode);
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_Valid_Data_Service_Update_Returns_NoErrors()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var userToUpdate = usersToRetrieve.First();
            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);
            var randomUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Update(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Returns(Task.FromResult(userToUpdate));
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToUpdate.UserTypeId)).Returns(Task.FromResult(userType));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Update(usersToRetrieve.First()).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestMethod]
        public void When_Update_Throws_Exception_But_Update_Returns_InternalServerError()
        {
            //Arrange

            var usersToRetrieve = GetRegularListOfUser();
            var userToUpdate = usersToRetrieve.First();
            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);
            var randomUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Update(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Throws(new Exception());
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToUpdate.UserTypeId)).Returns(Task.FromResult(userType));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Update(usersToRetrieve.First()).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.CustomResultCode);
            Assert.AreEqual(1, result.Errors.Count);
        }
        #endregion

        #region Create
        [TestMethod]
        public void When_Repository_Create_Returns_Errors_Service_Create_Returns_BadRequest()
        {
            //Arrange

            var userToCreate = new User
            {
                Name = "Test 15",
                Address = "Address 15",
                Email = "Email 15",
                Money = 1000,
                Phone = "",                 //This will break a validation rule and will create at least one error
                UserTypeId = 1,
            };
            var usersToRetrieve = GetRegularListOfUser();

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);
            var randomUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Add(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToCreate.UserTypeId)).Returns(Task.FromResult(userType));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Create(userToCreate).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.CustomResultCode);
            Assert.IsTrue(result.Errors.Any());
        }

        [TestMethod]
        public void When_Repository_Create_Has_NoErrors_Service_Create_Returns_NoErrors()
        {
            //Arrange

            var userToCreate = new User
            {
                Name = "Test 15",
                Address = "Address 15",
                Email = "Email 15",
                Money = 1000,
                Phone = "132456789",
                UserTypeId = 1,
            };

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var gifUserTypes = GetGIFUserTypes().Where(x => x.UserTypeId == userToCreate.UserTypeId).AsEnumerable();

            var randomUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Add(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToCreate.UserTypeId)).Returns(Task.FromResult(userType));
            unitOfWorkMock.Setup(x => x.GIFUserTypes.GetAllByUserTypeId(userToCreate.UserTypeId)).Returns(Task.FromResult(gifUserTypes));

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Create(userToCreate).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.IsTrue(!result.Errors.Any());
            Assert.IsNotNull(result.SingleResult);
            Assert.IsInstanceOfType(result.SingleResult, typeof(User));
        }

        [TestMethod]
        public void When_Repository_Create_ThrowsException_Service_Update_Returns_InternalServerError()
        {
            //Arrange

            var userToCreate = new User
            {
                Name = "Test 15",
                Address = "Address 15",
                Email = "Email 15",
                Money = 1000,
                Phone = "132456789",
                UserTypeId = 1,
            };

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);
            var randomUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.Add(It.IsAny<User>())).Verifiable();
            unitOfWorkMock.Setup(x => x.UserTypes.GetById(userToCreate.UserTypeId)).Returns(Task.FromResult(userType));
            unitOfWorkMock.Setup(x => x.GIFUserTypes.GetAllByUserTypeId(userToCreate.UserTypeId)).Throws(new Exception());

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Create(userToCreate).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.CustomResultCode);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsNull(result.SingleResult);

        }
        #endregion

        #region Delete
        [TestMethod]
        public void When_Repository_Delete_ThrowsException_Service_Delete_Returns_InternalServerError()
        {
            //Arrange

            var randomValidUserId = new Random().Next(100, 1000);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Throws(new Exception());

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Delete(randomValidUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.CustomResultCode);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsNull(result.SingleResult);
        }

        [TestMethod]
        public void When_Repository_Delete_Has_NonValid_UserId_Service_Delete_Returns_InternalServerError()
        {
            //Arrange

            var invalidUserId = 0;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Throws(new Exception());

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Delete(invalidUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.CustomResultCode);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsNull(result.SingleResult);

        }

        [TestMethod]
        public void When_Repository_WorksFine_Service_Delete_Returns_NoError()
        {
            //Arrange
            var userToDelete = GetRegularListOfUser().First();
            var validUserId = userToDelete.UserId;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetById(It.IsAny<int>())).Returns(Task.FromResult(userToDelete));
            unitOfWorkMock.Setup(x => x.Users.Delete(It.IsAny<User>())).Verifiable();

            var userService = new UserService(unitOfWorkMock.Object);

            //ACT
            var result = userService.Delete(validUserId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.IsFalse(result.Errors.Any());
        }
        #endregion

        private static List<User> GetRegularListOfUser()
        {
            return new List<User>()
            {
                new User{
                Address = "Some Address",
                Email = "Some Email",
                Money = 15.2M,
                Name = "Test",
                Phone = "123456",
                UserId = 156,
                    UserTypeId = 1
                },
                new User{
                Address = "Some Address 2 ",
                Email = "Some Email 2 ",
                Money = 99.2M,
                Name = "Test 2",
                Phone = "789456",
                UserId = 465,
                    UserTypeId =2
                }
            };
        }

        private static List<UserType> GetUserTypes()
        {
            return new List<UserType>()
            {
                new UserType{UserTypeId = 1, Name = "Normal" },
                new UserType{UserTypeId = 2, Name = "SuperUser"},
                new UserType{UserTypeId = 3, Name = "Premium"},
            };
        }

        private static List<GIFUserType> GetGIFUserTypes()
        {
            return new List<GIFUserType>()
            {
                new GIFUserType
                {
                    GIFUserTypeId = 1,
                    UserTypeId=1,
                    LowerLimit= 100,
                    UpperLimit= int.MaxValue,
                    GIF = 0.12M
                },
                new GIFUserType
                {
                    GIFUserTypeId = 2,
                    UserTypeId=1,
                    LowerLimit= 10,
                    UpperLimit= 100,
                    GIF = 0.8M
                },
                new GIFUserType
                {
                    GIFUserTypeId = 3,
                    UserTypeId=2,
                    LowerLimit= 100,
                    UpperLimit= int.MaxValue,
                    GIF = 0.2M
                },
                new GIFUserType
                {
                    GIFUserTypeId = 4,
                    UserTypeId=3,
                    LowerLimit= 100,
                    UpperLimit= int.MaxValue,
                    GIF = 2M
                },
            };
        }
    }
}
