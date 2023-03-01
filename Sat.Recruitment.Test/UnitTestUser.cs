using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Services.Commands;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTestUser
    {
        private readonly UsersController userController;

        public UnitTestUser(UsersController userController)
        {
            this.userController = userController;
        }

        [Fact]
        public async void Test1()
        {
            var createUserCommand = new CreateUserCommand()
            {
                name = "Mike",
                email = "mike@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            };
            var result = await userController.CreateUser(createUserCommand);


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async void Test2()
        {
            var createUserCommand = new CreateUserCommand()
            {
                name = "Agustina",
                email = "Agustina@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            };
            var result = await userController.CreateUser(createUserCommand);


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
