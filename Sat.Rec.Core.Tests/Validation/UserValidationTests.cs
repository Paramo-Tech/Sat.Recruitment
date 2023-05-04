using Moq;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Core.Validation;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Tests.Validation
{
    [TestClass]
    public class UserValidationTests
    {
        #region ValidateCreate

        [TestMethod]
        public void When_ValidateCreate_Gets_Empty_Address_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.Address = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }
        [TestMethod]
        public void When_ValidateCreate_Gets_Null_Address_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.Address = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }


        [TestMethod]
        public void When_ValidateCreate_Gets_Empty_Name_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.Name = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_Gets_Null_Name_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.Name = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }


        [TestMethod]
        public void When_ValidateCreate_Gets_Empty_Email_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.Email = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_Gets_null_Email_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.Email = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_userTypeIdEqualsToZero_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.UserTypeId = 0;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }
        [TestMethod]
        public void When_ValidateCreate_userTypeIdGreaterThanZero_But_NoMatchWithUserTypes_Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var userToCreate = GetValidCreateTestUser();
            userToCreate.UserTypeId = int.MaxValue;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult((UserType)null));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_Has_AlreadyExistant_Address_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToCreate = GetValidCreateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult(existingUser));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_Has_AlreadyExistant_Email_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToCreate = GetValidCreateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(existingUser));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_Has_AlreadyExistant_Name_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToCreate = GetValidCreateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult(existingUser));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateCreate_Has_AlreadyExistant_Phone_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToCreate = GetValidCreateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToCreate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult(existingUser));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToCreate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        #endregion


        #region ValidateUpdate
        [TestMethod]
        public void When_ValidateUpdate_Gets_userId_LessThanOne_Address_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.UserId = 0;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Gets_Empty_Address_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Address = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Gets_Null_Address_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Address = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Gets_Empty_Name_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Name = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }
        [TestMethod]
        public void When_ValidateUpdate_Gets_Null_Name_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Name = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Gets_Empty_Email_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Email = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }
        [TestMethod]
        public void When_ValidateUpdate_Gets_Null_Email_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Email = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Gets_Empty_Phone_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Phone = string.Empty;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }
        [TestMethod]
        public void When_ValidateUpdate_Gets_Null_Phone_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.Phone = null;

            var userType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Gets_userTypeId_LessThanOne_Address_Returns_ValidationResult_With_Errors()
        {
            //Arrange

            var userToUpdate = GetValidUpdateTestUser();
            userToUpdate.UserTypeId = 0;

            var userType = (UserType)null;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(userType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(2, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Has_AlreadyExistant_Address_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToUpdate = GetValidUpdateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult(existingUser));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToUpdate.ValidateCreate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Has_AlreadyExistant_Email_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToUpdate = GetValidUpdateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);
            
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(existingUser));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Has_AlreadyExistant_Name_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToUpdate = GetValidUpdateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult(existingUser));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void When_ValidateUpdate_Has_AlreadyExistant_Phone_But__Returns_ValidationResult_With_NoErrors()
        {
            //Arrange

            var existingUser = GetRegularListOfUser().First();
            var userToUpdate = GetValidUpdateTestUser();
            var existingUserType = GetUserTypes().First(x => x.UserTypeId == userToUpdate.UserTypeId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Users.GetByAddress(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            unitOfWorkMock.Setup(x => x.Users.GetByPhone(It.IsAny<string>())).Returns(Task.FromResult(existingUser));
            unitOfWorkMock.Setup(x => x.Users.GetByName(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            unitOfWorkMock.Setup(x => x.UserTypes.GetById(It.IsAny<int>())).Returns(Task.FromResult(existingUserType));

            //ACT
            var result = userToUpdate.ValidateUpdate(unitOfWorkMock.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ValidationResult<User>));
            Assert.AreEqual(1, result.Errors.Count);
        }
        #endregion

        private User GetValidCreateTestUser()
        {
            return new User
            {
                Address = "Some Address",
                Email = "luis.montano+barrios@gmail.com",
                Money = 15.2M,
                Name = "Luis Montano",
                Phone = "301123456789",
                UserTypeId = 1
            };
        }

        private User GetValidUpdateTestUser()
        {
            return new User
            {
                UserId = 999,
                Address = "Some Address",
                Email = "luis.montano+barrios@gmail.com",
                Money = 15.2M,
                Name = "Luis Montano",
                Phone = "301123456789",
                UserTypeId = 1
            };
        }

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

        private static List<UserType> GetUserTypes()
        {
            return new List<UserType>()
            {
                new UserType{UserTypeId = 1, Name = "Normal" },
                new UserType{UserTypeId = 2, Name = "SuperUser"},
                new UserType{UserTypeId = 3, Name = "Premium"},
            };
        }
    }
}
