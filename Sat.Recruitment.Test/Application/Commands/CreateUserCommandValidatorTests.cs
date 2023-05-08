using FluentValidation;
using Sat.Recruitment.Application.Command;
using Sat.Recruitment.Domain.Enum;
using Xunit;

namespace Sat.Recruitment.Test.Application.Commands
{
    public class CreateUserCommandValidatorTests
    {
        [Fact]
        public void ValidateCreateUserCommand_WithValidInput_SucceedsValidation()
        {
            // Arrange.
            var command = new CreateUserCommand()
            {
                Name = "John Doe",
                Address = "Fake St 123",
                Email = "johndoe@gmail.com",
                Phone = "+5491112121212",
                Money = 100M,
                UserType = UserType.Normal
            };

            var validator = new CreateUserCommandValidator();

            // Act.
            var result = validator.Validate(command);

            // Assert.
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("", "", "", "")]
        [InlineData("", null, "", null)]
        public void ValidateCreateUserCommand_WithInvalidInput_ThrowsException(string name, string address, string email, string phone)
        {
            // Arrange.
            var command = new CreateUserCommand()
            {
                Address = address,
                Email = email, 
                Phone = phone,
                Name = name,
                UserType = UserType.Normal
            };

            var validator = new CreateUserCommandValidator();

            // Act.
            void validation() => validator.ValidateAndThrow(command);

            // Assert.
            Assert.Throws<ValidationException>(validation);
        }
    }
}
