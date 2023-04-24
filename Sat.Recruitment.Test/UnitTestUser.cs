using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Sat.Recruitment.Business.User.CreateUser;
using Sat.Recruitment.Persistence.Interfaces;
using Sat.Recruitment.Persistence.Repositories;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTestUser
    {
        private readonly UserFileRepository userFileRepository;
        private readonly Mock<IUserFileRepository> userFileRepositoryMock;

        public UnitTestUser()
        {
            userFileRepositoryMock = new Mock<IUserFileRepository>();
            userFileRepository = new UserFileRepository(userFileRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldReturnSucceededCreateUser()
        {
            //arrange
            var createUserCommand = new CreateUserCommand()
            {
                name = "pablo",
                email = "pablo@gmail.com",
                address = "Av. Godoy Cruz",
                phone = "1122334455",
                userType = "Normal",
                money = "333"
            };

            //Act
            IRequestHandler<CreateUserCommand, CreateUserResult> handler = new CreateUserCommandHandler(userFileRepository);
            CreateUserResult result = await handler.Handle(createUserCommand, CancellationToken.None);

            //Result
            Assert.True(result.Success);
            Assert.Equal("User Created", result.Errors);

        }

        [Fact]
        public async Task ThrowErrorCreateUserDuplicated()
        {
            //Arrange
            var createUserCommand = new CreateUserCommand()
            {
                name = "Agustina",
                email = "Agustina@gmail.com",
                address = "Garay y Otra Calle",
                phone = "+534645213542",
                userType = "SuperUser",
                money = "112234"
            };

            //Act
            IRequestHandler<CreateUserCommand, CreateUserResult> handler = new CreateUserCommandHandler(userFileRepository);
            CreateUserResult result = await handler.Handle(createUserCommand, CancellationToken.None);


            //Result
            Assert.False(result.Success);

        }
    }
}
