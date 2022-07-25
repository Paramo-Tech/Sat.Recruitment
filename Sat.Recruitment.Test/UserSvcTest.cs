using Moq;
using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.BusinessLogic.Services;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [Collection("Users")]
    public class UserSvcTest
    {
        private UserModel userModel;
        private readonly List<User> listUsers;

        public UserSvcTest()
        {
            #region Global Pre-Arrange
            listUsers = new List<User>
            {
                new User()
                {
                    Id = System.Guid.NewGuid(),
                    Name = "Andres",
                    Address = "Juan B Justo",
                    Email = "andresmiretti@gmail.com",
                    Money = 1233445,
                    Phone = "+54923889920",
                    UserType = Models.Enums.UserType.SuperUser
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

        [Fact(DisplayName = "Save with complete and correct info, must return success")]
        public async void Save_WhenCompleteAndCorrectInfo_MustReturnSuccess()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Save(It.IsAny<User>())).Returns(Task.Run(() => true));
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));
            
            UserSvc svc = new UserSvc(userRepositoryMock.Object); 

            //Act
            var result = await svc.Save(userModel);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User created successfully!", result.Message);
        }

        [Fact(DisplayName = "Save with complete info, but existing email, must return no success")]
        public async void Save_WhenCompleteInfoButExistingEmail_MustReturnSuccessFalse()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Save(It.IsAny<User>())).Returns(Task.Run(() => true));
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));
            userModel.Email = listUsers.First().Email;

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Save(userModel);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
        }

        [Fact(DisplayName = "Save with complete info, but existing phone, must return no success")]
        public async void Save_WhenCompleteInfoButExistingPhone_MustReturnSuccessFalse()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Save(It.IsAny<User>())).Returns(Task.Run(() => true));
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));
            userModel.Phone = listUsers.First().Phone;

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Save(userModel);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
        }

        [Fact(DisplayName = "Save with complete info, but existing name and address, must return no success")]
        public async void Save_WhenCompleteInfoButExistingNameAndAddress_MustReturnSuccessFalse()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Save(It.IsAny<User>())).Returns(Task.Run(() => true));
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));
            
            userModel.Name = listUsers.First().Name;
            userModel.Address = listUsers.First().Address;

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Save(userModel);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
        }

        [Fact(DisplayName = "Save whith complete and correct info but with the name repeated, must return success")]
        public async void Save_WhenCompleteAndCorrectInfoButRepeatedName_MustReturnSuccess()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Save(It.IsAny<User>())).Returns(Task.Run(() => true));
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));

            userModel.Name = listUsers.First().Name;

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Save(userModel);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User created successfully!", result.Message);
        }

        [Fact(DisplayName = "Save whith complete and correct info but with the address repeated, must return success")]
        public async void Save_WhenCompleteAndCorrectInfoButRepeatedAddress_MustReturnSuccess()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Save(It.IsAny<User>())).Returns(Task.Run(() => true));
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));

            userModel.Address = listUsers.First().Address;

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Save(userModel);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User created successfully!", result.Message);
        }

        [Fact(DisplayName = "Get All. When try to get all items, must return list of users")]
        public async void GetAll_WhenGetAllItems_MustReturnListOfUsers()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetAll()).Returns(Task.Run(() => listUsers));

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.GetAll();

            //Assert
            Assert.True(result.Count > 0);
        }

        [Fact(DisplayName = "Get user by Id. When try to get a item by Id, must return a user")]
        public async void GetById_WhenGetAItemWithId_MustReturnAUser()
        {
            //Arrange
            var user = listUsers.First();
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Get(user.Id)).Returns(Task.Run(() => user));

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Get(user.Id);

            //Assert
            Assert.IsAssignableFrom<User>(result);
            Assert.Equal(result.Email, user.Email);
            Assert.Equal(result.Name, user.Name);
        }

        [Fact(DisplayName = "Get user by Id. When try to get a no existing item by Id, must return null")]
        public async void GetById_WhenGetAItemWithId_MustReturnNull()
        {
            //Arrange
            var user = listUsers.First();
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Get(user.Id)).Returns(Task.Run(() => user));

            UserSvc svc = new UserSvc(userRepositoryMock.Object);

            //Act
            var result = await svc.Get(System.Guid.NewGuid());

            //Assert
            Assert.True(result == null);
        }
    }
}
