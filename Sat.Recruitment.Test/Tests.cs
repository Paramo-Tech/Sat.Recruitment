using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CQS.Commands;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Moq;
using Sat.Recruitment.Api.Controllers;

using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class Tests
    {
        [Fact]
        public async Task HappyPath()
        {
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(_ => _.Exists(It.IsAny<CreateNewUserDTO>())).ReturnsAsync((CreateNewUserDTO dto) =>
            {
                return false;
            });

            userRepoMock.Setup(_ => _.Add(It.IsAny<User>())).Returns((User entity) =>
            {
                return Task.CompletedTask;
            });

            var command = new CreateNewUserCommand(userRepoMock.Object);
            var result = await command.Handle(new CreateNewUserDTO()
            {
                name = "Mike",
                email = "mike@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            }, cancellationToken: default);

            Assert.Equal(true, result.IsSuccess);
        }

        [Fact]
        public async Task AlreadyExists()
        {
            List<string> names = new List<string>();
            names.Add("Mike");

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(_ => _.Exists(It.IsAny<CreateNewUserDTO>())).ReturnsAsync((CreateNewUserDTO dto) =>
            {
                return names.Contains(dto.name);
            });

            var command = new CreateNewUserCommand(userRepoMock.Object);
            var result = await command.Handle(new CreateNewUserDTO()
            {
                name = "Mike",
                email = "mike@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            }, cancellationToken: default);

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public async Task BadParametersTest()
        {
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            var userController = new UsersController(mediatorMock.Object);
            var result = await userController.CreateUser(new CreateNewUserDTO()
            {
                // -- no params
            });

            Assert.Equal(false, result.IsSuccess);
            Assert.True(result.Errors.Contains("required"));
        }
    }
}
