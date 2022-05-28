using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Api.ApiModels;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Domain.Services;
using Sat.Recruitment.DataAccess.Implementation;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class IntegrationTests
    {
        private readonly UsersController _usersController;

        public IntegrationTests()
        {
            _usersController = new UsersController(new UserService(new FileUserRepository(GetFullPath),
                new UserBuilderDirectorDefaultService()));
        }

        [Fact]
        public async Task CreateUser_UserNotExist_ReturnSuccessResult()
        {
            var newUser = new CreateUserRequest()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };

            var result = await _usersController
                .CreateUser(newUser);


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async Task CreateUser_UserExist_ReturnNotSuccessResult()
        {
            var newUser = new CreateUserRequest()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };


            var result = await _usersController.CreateUser(newUser);
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        private static string GetFullPath()
        {
            const string filesUsersTxt = "/Files/Users.txt";
            var path = $"{Directory.GetCurrentDirectory()}{filesUsersTxt}";
            return path;
        }
    }
}