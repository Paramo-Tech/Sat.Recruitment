using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Models;
using Sat.Recruitment.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTest
    {
        readonly FileService fileService = new FileService();

        [Fact]
        public void IsValidUser()
        {
            fileService.ClearFile();
            var userController = new UsersController();

            var user = new User
            {

                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124

            };

            var result = userController.CreateUser(user).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void IsDuplicatedUserByEmail()
        {
            fileService.ClearFile();
            var userController = new UsersController();
            var user = new User
            {

                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124

            };           

            var result = userController.CreateUser(user).Result;

            var newUserController = new UsersController();
            var duplicatedUser = new User
            {

                Name = "Jorge",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan P",
                Phone = "+349 1122354245",
                UserType = "Normal",
                Money = 104

            };

            var finalResult = newUserController.CreateUser(duplicatedUser).Result;

            Assert.False(finalResult.IsSuccess);
            Assert.Equal("The user is duplicated", finalResult.Errors);
        }

        [Fact]
        public void IsDuplicatedUserByPhone()
        {
            fileService.ClearFile();
            var userController = new UsersController();
            var user = new User
            {

                Name = "Jorge",
                Email = "Jorge@gmail.com",
                Address = "Av. Juan P1",
                Phone = "+349 1122354245",
                UserType = "Normal",
                Money = 104

            };

            var result = userController.CreateUser(user).Result;

            var newUserController = new UsersController();
            var duplicatedUser = new User
            {

                Name = "Pedro",
                Email = "Pedro@gmail.com",
                Address = "Av. Juan P2",
                Phone = "+349 1122354245",
                UserType = "Premium",
                Money = 105

            };

            var finalResult = newUserController.CreateUser(duplicatedUser).Result;

            Assert.False(finalResult.IsSuccess);
            Assert.Equal("The user is duplicated", finalResult.Errors);
        }

        [Fact]
        public void IsDuplicatedUserByNameAndAddress()
        {
            fileService.ClearFile();
            var userController = new UsersController();
            var user = new User
            {

                Name = "Jorge",
                Email = "Jorge@gmail.com",
                Address = "Av. Juan P1",
                Phone = "+349 1122354245",
                UserType = "Normal",
                Money = 104

            };

            var result = userController.CreateUser(user).Result;

            var newUserController = new UsersController();
            var duplicatedUser = new User
            {

                Name = "Jorge",
                Email = "Jorgito@gmail.com",
                Address = "Av. Juan P1",
                Phone = "+349 1122354233",
                UserType = "Premium",
                Money = 105

            };

            var finalResult = newUserController.CreateUser(duplicatedUser).Result;

            Assert.False(finalResult.IsSuccess);
            Assert.Equal("The user is duplicated", finalResult.Errors);
        }
    }
}
