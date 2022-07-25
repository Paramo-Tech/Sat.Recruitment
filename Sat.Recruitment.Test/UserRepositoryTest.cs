using Moq;
using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.DataAccess.Repository;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [Collection("Users")]
    public class UserRepositoryTest
    {
        private readonly UserModel userModel;
        private readonly List<User> listUsers;

        public UserRepositoryTest()
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

        [Fact(DisplayName = "Save with complete and correct info, must return success")]
        public async void Save_WhenCompleteAndCorrectInfo_MustReturnSuccess()
        {
            //Arrange
            var userDataAccessMock = new Mock<IDataAccess>();
            userDataAccessMock.Setup(u => u.ReadData()).Returns(Task.Run(() => listUsers));
            userDataAccessMock.Setup(u => u.SaveData(It.IsAny<List<User>>())).Returns(Task.Run(() => true));

            UserRepository repo = new UserRepository(userDataAccessMock.Object);

            //Act
            var result = await repo.Save(new User(userModel));

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "When you try to get all items, must return list of users")]
        public async void GetAll_WhenTryToGetAllItems_MustReturnListOfUsers()
        {
            //Arrange
            var userDataAccessMock = new Mock<IDataAccess>();
            userDataAccessMock.Setup(u => u.ReadData()).Returns(Task.Run(() => listUsers));

            UserRepository repo = new UserRepository(userDataAccessMock.Object);

            //Act
            var result = await repo.GetAll();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsAssignableFrom<List<User>>(result);
        }

        [Fact(DisplayName = "When you try to get a existing item, must return a user")]
        public async void GetById_WhenTryToGetAExistingItemById_MustReturnAUser()
        {
            //Arrange
            var userDataAccessMock = new Mock<IDataAccess>();
            userDataAccessMock.Setup(u => u.ReadData()).Returns(Task.Run(() => listUsers));

            UserRepository repo = new UserRepository(userDataAccessMock.Object);

            //Act
            var result = await repo.Get(listUsers.First().Id);

            //Assert
            Assert.True(result != null);
            Assert.IsAssignableFrom<User>(result);
        }

        [Fact(DisplayName = "When you try to get a no existing item, must return null")]
        public async void GetById_WhenTryToGetANoExistingItemById_MustReturnNull()
        {
            //Arrange
            var userDataAccessMock = new Mock<IDataAccess>();
            userDataAccessMock.Setup(u => u.ReadData()).Returns(Task.Run(() => listUsers));

            UserRepository repo = new UserRepository(userDataAccessMock.Object);

            //Act
            var result = await repo.Get(Guid.NewGuid());

            //Assert
            Assert.True(result == null);
        }
    }
}