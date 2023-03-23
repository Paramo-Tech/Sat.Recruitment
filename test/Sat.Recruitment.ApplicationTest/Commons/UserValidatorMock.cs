using FluentValidation;
using FluentValidation.Results;
using Moq;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class UserValidatorMock
    {
        public static IValidator<User> UserValidatorNoError()
        {
            var mock = new Mock<IValidator<User>>();

            mock.Setup(x => x.ValidateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            return mock.Object;
        }

        public static IValidator<User> UserValidatorWithError()
        {
            var mock = new Mock<IValidator<User>>();

            mock.Setup(x => x.ValidateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()
                             {
                                 new ValidationFailure("TestField","Test Message"){ErrorCode = "1001"}
                             }));

            return mock.Object;
        }
    }
}
