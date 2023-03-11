using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Global.Interfaces;
using Sat.Recruitment.Global.WebContracts;
using Sat.Recruitment.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTests
    {
        private readonly IUsersService _usersService;

        public UserControllerTests()
        {
            _usersService = new UsersService();
        }

        [Fact]
        public void CreateUser_NewUser_Succeed()
        {
            var userController = new UsersController(_usersService);

            var newUser = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(newUser);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void CreateUser_InvalidEmail_Error()
        {
            var userController = new UsersController(_usersService);

            var newUser = new User("Mike", "wrongemail", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(newUser);

            Assert.False(result.IsSuccess);
            Assert.Equal("Not valid Email", result.Errors);
        }

        [Fact]
        public void CreateUser_Duplicated_Error()
        {
            var userController = new UsersController(_usersService);

            var newUser = new User("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(newUser);

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}