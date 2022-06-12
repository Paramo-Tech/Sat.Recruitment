using Sat.Recruitment.Api.Controllers;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTest
    {
        [Fact]
        public void Create_WhenRequestIsValidAndUserNotExists_UserCreated()
        {
            var userController = new UsersController();
            var createUserDto = new CreateUserDto()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var result = userController.CreateUser(createUserDto).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Create_WhenRequestIsValidAndUserExists_UserNotCreated()
        {
            var userController = new UsersController();
            var createUserDto = new CreateUserDto()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var result = userController.CreateUser(createUserDto).Result;


            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
