using Sat.Recruitment.Api.Controllers;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private UsersController userController = new UsersController();
        [Fact]
        public void Test1()
        {
            // Arrange
            var userName = "Mike";
            var email = "mike@gmail.com";
            var address = "Av. Juan G";
            var phone = "+349 1122354215";
            var userType = "Normal";
            var money = "124";

            // Act
            var result = userController.CreateUser(userName, email, address, phone, userType, money).Result;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            var userName = "Agustina";
            var email = "Agustina@gmail.com";
            var address = "Av. Juan G";
            var phone = "+349 1122354215";
            var userType = "Normal";
            var money = "124";

            // Act
            var result1 = userController.CreateUser(userName, email, address, phone, userType, money).Result;
            var result2 = userController.CreateUser(userName, email, address, phone, userType, money).Result;

            // Assert
            Assert.False(result2.IsSuccess);
            Assert.Equal("The user is duplicated", result2.Errors);
        }
    }
}
