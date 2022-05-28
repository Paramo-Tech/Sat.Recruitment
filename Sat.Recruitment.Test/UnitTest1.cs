using System.Threading.Tasks;
using Sat.Recruitment.Api.ApiModels;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DataAccess.Implementation;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Domain.Services;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public async  Task Test1()
        {
            var userController = new UsersController(new UserService(new UserRepository(), new UserBuilderDirectorDefaultService()));

            var newUser = new CreateUserRequest()
            {
                Name = "Mike", 
                Email = "mike@gmail.com", 
                Address = "Av. Juan G", 
                Phone = "+349 1122354215",
                UserType =  UserType.Normal,
                Money = 124
            };

            var result = await userController
                .CreateUser(newUser);


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async  Task Test2()
        {
            var userController = new UsersController(new UserService(new UserRepository(), new UserBuilderDirectorDefaultService()));
            
            var newUser = new CreateUserRequest()
            {
                Name = "Agustina", 
                Email = "Agustina@gmail.com", 
                Address = "Av. Juan G", 
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };


            var result = await userController.CreateUser(newUser);
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}