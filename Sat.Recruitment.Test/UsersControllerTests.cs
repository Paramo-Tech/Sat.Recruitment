using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Features.Users;
using Sat.Recruitment.Api.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        [Fact]
        public async Task UserCreationShouldWorkFineWhenPassingCorrectData()
        {
            var userController = new UsersController(new MockUserDataService(), new FakeLogger<UsersController>());

            var result = await userController.CreateUser(
                new CreateUserRequest
                {
                    Name = "Mike",
                    Email = "mike@gmail.com",
                    Address = "Av. Juan G",
                    Phone = "+349 1122354215",
                    UserType = UserType.Normal,
                    Money = 124M
                });

            var createdResult = Assert.IsType<ObjectResult>(result);
            var envelope = createdResult.Value as Envelope<string>;
            Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
            Assert.NotNull(envelope);
            Assert.False(envelope.HasError);
            Assert.Equal("User Created", envelope.Result);
        }

        [Fact]
        public async Task UserCreationShouldDetectDuplicatedUser()
        {
            var userController = new UsersController(new MockUserDataService(), new FakeLogger<UsersController>());

            var result = await userController.CreateUser(
                new CreateUserRequest
                { 
                    Name = "Agustina",
                    Email = "Agustina@gmail.com",
                    Address = "Av. Juan G",
                    Phone = "+349 1122354215",
                    UserType = UserType.Normal,
                    Money = 124M
                });

            var badRequestResult = Assert.IsType<ObjectResult>(result);
            var envelope = badRequestResult.Value as Envelope;
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
            Assert.NotNull(envelope);
            Assert.True(envelope.HasError);
            Assert.Equal("The user is duplicated", envelope.ErrorMessage);
        }
    }
}
