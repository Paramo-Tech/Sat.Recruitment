using Moq;
using Shared.Domain;
using Users.Application.Commands.Create;
using Users.Domain;
using Users.Domain.UserGif.Calculators;
using Users.Domain.UserGif.Getter;
using Xunit;

namespace Users.UnitTest.Application.Commands.Create
{
    public class CreateUserCoammndHandlerTest
    {
        private readonly Mock<IGifCalculateGetter> gifCalculateGetterMock = new();
        private readonly Mock<IUserRepository> userRepositoryMock = new();
        private CreateUserCommandHandler createUserCoammndHandler;

        public CreateUserCoammndHandlerTest()
        {
            this.gifCalculateGetterMock
                .Setup(x => x.GetCalculator(It.IsAny<UserType>()))
                .Verifiable();

            this.userRepositoryMock
                .Setup(x => x.Search(It.IsAny<ISpecification<User>>()))
                .Verifiable();

            this.createUserCoammndHandler = new(
                this.gifCalculateGetterMock.Object,
                this.userRepositoryMock.Object);
        }

        [Theory]
        [MemberData(nameof(DataProvider.InvalidCommands), MemberType = typeof(DataProvider))]
        public void Handle_WhenCommandIsInvalid_ExceptionExpected(CreateUserCommand command)
        {
            Assert.ThrowsAsync<ApplicationException>(
              () => this.createUserCoammndHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public void Handle_WhenCommandIsValidAndUserAlreadyExists_ExceptionExpected()
        {
            this.userRepositoryMock
                .Setup(x => x.Search(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(new User());

            var validCommand = DataProvider.ValidCommand();

            Assert.ThrowsAsync<ApplicationException>(
               () =>  this.createUserCoammndHandler.Handle(validCommand, CancellationToken.None));

            this.userRepositoryMock
                .Verify(x => x.Search(It.IsAny<ISpecification<User>>()), Times.Once());
        }

        [Fact]
        public async Task Handle_WhenCommandIsValidAndUserNotExists_Success()
        {
            this.gifCalculateGetterMock
                .Setup(x => x.GetCalculator(It.IsAny<UserType>()))
                .Returns(new CalculateNormalUserGif());

            this.userRepositoryMock
                .Setup(x => x.Search(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(() => null);

            var validCommand = DataProvider.ValidCommand();

            await this.createUserCoammndHandler.Handle(validCommand, CancellationToken.None);

            this.gifCalculateGetterMock
                .Verify(x => x.GetCalculator(It.IsAny<UserType>()), Times.Once());
            this.userRepositoryMock
                .Verify(x => x.Search(It.IsAny<ISpecification<User>>()), Times.Once());
        }
    }
}
