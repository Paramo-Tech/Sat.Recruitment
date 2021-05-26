using Domain;
using Sat.Recruitment.Api.Controllers;
using Xunit;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUserTest
    {
        [Fact]
        public async Task Create_User_SuccessfullyAsync()
        {
            //arrange
            var userController = new UsersController();
            var userCorrect = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };

            //act
            var result = await userController.CreateUserAsync(userCorrect);

            //assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Create_Duplicate_UserAsync()
        {
            //arrange
            var userController = new UsersController();
            var user_duplicated = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };

            //act
            var result = await userController.CreateUserAsync(user_duplicated);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The user is duplicated", result.Errors);
        }
    }
}
