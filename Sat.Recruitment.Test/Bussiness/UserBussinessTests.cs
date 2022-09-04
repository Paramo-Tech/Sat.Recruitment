using Moq;
using Sat.Recruitment.Bussiness;
using Sat.Recruitment.Infrastructure.Interfaces.DataAccess;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UseBussinessTests", DisableParallelization = true)]
    public class UserBussinessTests
    {
        private readonly UserBussiness _userBussiness;
        private readonly Mock<IUserDataAccess> _mockUserDataAccess;

        public UserBussinessTests()
        {
            _mockUserDataAccess = new Mock<IUserDataAccess>();

            _userBussiness = new UserBussiness(_mockUserDataAccess.Object);
        }


        [Fact]
        public void GivenUserCreation_WhenUserHasSameNameAnotherAddress_ThenUserShouldBeCreatedr()
        {
            var userToInsert = new User
            {
                Name = "Mike",
                Email = "mike2@gmail.com",
                Address = "Av. Juan G1",
                Phone = "+349 112323954215",
                UserType = "Normal",
                OriginalMoney = 124
            };

            _mockUserDataAccess.Setup(dataAccess => dataAccess.GetSingleBy(It.IsAny<Func<User,bool>>())).Returns<User>(null);
            User actualUser=null;
            _mockUserDataAccess.Setup(dataAccess => dataAccess.CreateEntity(It.IsAny<User>())).Callback<User>((user) => actualUser = user);

            _userBussiness.CreateUser(userToInsert);

            Assert.NotNull(actualUser);
            Assert.Equal(userToInsert.Name, actualUser.Name);
            Assert.Equal(userToInsert.Email, actualUser.Email);
            Assert.Equal(userToInsert.Phone, actualUser.Phone);
            Assert.Equal(userToInsert.Address, actualUser.Address);
            Assert.Equal(userToInsert.UserType, actualUser.UserType);
            
        }

        [Theory]
        [InlineData(null, "+349 11223542115", "Av. Juan G", "Mike", " The email is required")]//email 
        [InlineData("mike@gmail.com", null, "Av. Juan G", "Mike", " The phone is required")]//phone
        [InlineData("mike@gmail.com", "+349 11223542115", null, "Mike", " The address is required")]//address
        [InlineData("mike@gmail.com", "+349 11223542115", "Av. Juan G", null, "The name is required")]//name
        public void GivenUserCreation_WhenUserHasInvalidData_ThenItWillGetAnError(string email, string phone, string address, string name, string expectedErrorMessage)
        {            
            Action act = () =>
              _userBussiness.CreateUser(new User { Name = name, Email = email, Address = address, Phone = phone, UserType = "Normal", OriginalMoney = 124 });
            var exception = Assert.Throws<Exception>(act);


            Assert.NotNull(exception);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void GivenUserCreation_WhenUserEmailHasPlusChar_ThenUserEmailShouldBeStoredNormalize()
        {
            User actualUser = null;
            var expectedEmail = "mike@gmail.com";

            _mockUserDataAccess.Setup(dataAccess => dataAccess.GetSingleBy(It.IsAny<Func<User, bool>>())).Returns<User>(null);
            Action<User> action=(user)=>actualUser = user;
            _mockUserDataAccess.Setup(dataAccess => dataAccess.CreateEntity(It.IsAny<User>())).Callback(action);

            _userBussiness.CreateUser(new User { Name = "Mike", Email = "mike+1@gmail.com", Address = "Av. Juan G", Phone = "+3491122354215", UserType = "Normal", OriginalMoney = 124 });

            Assert.Equal(expectedEmail, actualUser.Email);
        }

        [Theory]
        [InlineData("mike@gmail.com", "+349 11223542115", "Av. Juan G2", "Mike2", "The user is duplicated")]//email 
        [InlineData("mike@gmail.comd", "+349 1122354215", "Av. Juan G3", "Mike2", "The user is duplicated")]//phone
        [InlineData("mike@gmail.com2", "+349 1122354215d", "Av. Juan G", "Mike", "The user is duplicated")]
        public void GivenUserCreation_WhenUserisDuplicated_ThenItWillGetAnErrorAndNoUserShouldBeAdded(string email, string phone, string address, string name, string expectedErrorMessage)
        {
            var originalUser = new User { Name = name, Email = email, Address = address, Phone = phone, UserType = "Normal", OriginalMoney = 124 };

            _mockUserDataAccess.Setup(dataAccess => dataAccess.GetSingleBy(It.IsAny<Func<User, bool>>())).Returns(new User());

            Action createUserAction = () => _userBussiness.CreateUser(new User { Name = name, Email = email, Address = address, Phone = phone, UserType = "Normal", OriginalMoney = 124 });
            var actualExceptionThrown = Assert.Throws<Exception>(createUserAction);

            Assert.Equal(expectedErrorMessage, actualExceptionThrown.Message);
        }



        [Theory]
        [InlineData(100, "Normal", 100)]
        [InlineData(101, "Normal", 113.12)]
        [InlineData(99, "Normal", 178.2)]

        [InlineData(100, "SuperUser", 100)]
        [InlineData(101, "SuperUser", 121.2)]
        [InlineData(99, "SuperUser", 99)]

        [InlineData(100, "Premium", 100)]
        [InlineData(101, "Premium", 303)]
        [InlineData(99, "Premium", 99)]
        public void GivenUserCreation_WhenUserHAsDifferentUserType_ThenMoneyWillBeChanged(decimal money, string userType, decimal expectedMoney)
        {

            User actualUser = null;

            _mockUserDataAccess.Setup(dataAccess => dataAccess.GetSingleBy(It.IsAny<Func<User, bool>>())).Returns<User>(null);
            Action<User> action = (user) => actualUser = user;
            _mockUserDataAccess.Setup(dataAccess => dataAccess.CreateEntity(It.IsAny<User>())).Callback(action);

            _userBussiness.CreateUser(new User { Name = "Mike", Email = "mike+1@gmail.com", Address = "Av. Juan G", Phone = "+3491122354215", UserType = userType, OriginalMoney = money });

            Assert.Equal(expectedMoney, actualUser.Money);
        }

    }
}
