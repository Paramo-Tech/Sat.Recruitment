using Sat.Recruitment.Application.Commands.CreateUser;
using Sat.Recruitment.ApplicationTest.Commons;
using Xunit;
using ValidationException = Sat.Recruitment.Application.Exceptions.ValidationException;

namespace Sat.Recruitment.ApplicationTest.CommandsTests.CreateUserTest
{
    public class CreateUserCommandHandlerTest
    {
        [Fact]
        public async void createuser_success()
        {
            var command = CommandMock.SomeUser;
            var repository = UserRepositoryMock.GetUserRepositoryMock();
            var logger = LoggerMock.Log();
            var validator = UserValidatorMock.UserValidatorNoError();
            var mapper = MappingMock.Mapping();

            var handler = new CreateUserCommandHandler(repository, logger, mapper, validator);

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.Id == 1);
        }

        [Fact]
        public async void createuser_witherros()
        {
            var command = CommandMock.SomeUser;
            var repository = UserRepositoryMock.GetUserRepositoryMock();
            var logger = LoggerMock.Log();
            var validator = UserValidatorMock.UserValidatorWithError();
            var mapper = MappingMock.Mapping();

            var handler = new CreateUserCommandHandler(repository, logger, mapper, validator);

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
