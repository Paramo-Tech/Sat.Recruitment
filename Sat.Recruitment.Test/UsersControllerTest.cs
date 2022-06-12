using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Commands.Create;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTest
    {
        private readonly Mock<ILogger<UsersController>> loggerMock = new ();
        private readonly Mock<IMediator> mediatorMock = new ();
        private UsersController usersController;

        public UsersControllerTest()
        {
            this.usersController = new (
                this.loggerMock.Object,
                this.mediatorMock.Object);
        }

        [Fact]
        public async Task Create_WhenRequestIsValid_CommandIsSent()
        {
            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .Verifiable();

            var createUserDto = DataProvider.ValidCreateUserRequest();

            var result = await this.usersController.CreateUserAsync(createUserDto);

            this.mediatorMock
                .Verify(x => x
                    .Send(
                    It.Is<CreateUserCommand>(command =>
                        command.Name.Equals(createUserDto.Name) && 
                        command.Email.Value.Equals(createUserDto.Email) && 
                        command.Phone.Value.Equals(createUserDto.Phone) && 
                        command.Address.Equals(createUserDto.Address) && 
                        command.UserType.Value.Equals(createUserDto.UserType) && 
                        command.Money.Equals(createUserDto.Money) 
                    ), It.IsAny<CancellationToken>()), Times.Once());

            var okRequestResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okRequestResult.StatusCode);
        }
    }
}
