
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Controllers.Entity;
using Sat.Recruitment.Api.Logic;
using Sat.Recruitment.Api.Logic.Entity;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void TestCreateUser()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "90"
            };

            var result = userController.CreateUser(user);


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public void TestDuplicateUserEmail()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Agustina",
                Phone = "+349 11223542155",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Message);

        }
        [Fact]
        public void TestDuplicateUserPhone()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+5491154762312",
                Email = "Mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Message);

        }
        [Fact]
        public void TestDuplicateUserNameAddres()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Juan",
                Phone = "+349 11223542155",
                Email = "Juan@marmol.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("User is duplicated", result.Message);

        }
        [Fact]
        public void TestNameRequerid()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "",
                Phone = "+349 11223542155",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Name is required.", result.Message);

        }
        [Fact]
        public void TestPhomeRequerid()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Phone is required.", result.Message);

        }
        [Fact]
        public void TestEmailRequerid()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email is required.", result.Message);

        }
        [Fact]
        public void TestAddresRequerid()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "",
                UserType = "Normal",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Address is required.", result.Message);

        }
        [Fact]
        public void TestUserTRequerid()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("UserType is required.", result.Message);

        }
        [Fact]
        public void TestMoneyRequerid()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = ""
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Money is required.", result.Message);

        }
        [Fact]
        public void TestEmailFormat()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "120"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email", result.Message);

        }
        [Fact]
        public void TestInvaUserT()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normaln",
                Money = "124"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid UserType", result.Message);
        }

        [Fact]
        public void TestMoneyNormal()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Normal",
                Money = "9"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid money", result.Message);
        }
        [Fact]
        public void TestMoneySuper()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "SuperUser",
                Money = "99"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid money", result.Message);
        }
        [Fact]
        public void TestMoneyPremium()
        {
            var userController = new UsersController();

            RequestUser user = new RequestUser
            {
                Name = "Mike",
                Phone = "+349 11223542155",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                UserType = "Premium",
                Money = "99"
            };

            var result = userController.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid money", result.Message);
        }

        [Fact]
        public void TestDecimalMoneyNormal()
        {
            User user = new User();
            decimal money = user.setMoney("120");

            Assert.True(true);
            Assert.Equal("134.40", money.ToString());
        }
        [Fact]
        public void TestDecimalMoneySuper()
        {
            User user = new SuperUser();
            decimal money = user.setMoney("120");

            Assert.True(true);
            Assert.Equal("144.0", money.ToString());
        }
        [Fact]
        public void TestDecimalMoneyPremium()
        {
            User user = new PremiumUser();
            decimal money = user.setMoney("120");

            Assert.False(false);
            Assert.Equal("360", money.ToString());
        }
    }
}
