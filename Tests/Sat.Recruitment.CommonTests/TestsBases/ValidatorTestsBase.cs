using FluentValidation.Results;
using Xunit;

namespace Sat.Recruitment.CommonTests.TestsBases
{
    public class ValidatorTestsBase
    {
        public static void Validate_Ok(ValidationResult fluentValidationResult)
        {
            Assert.NotNull(fluentValidationResult);
            Assert.True(fluentValidationResult.IsValid);
            Assert.Empty(fluentValidationResult.Errors);

        }

        public static void Validate_Error(ValidationResult fluentValidationResult, string propertyName)
        {
            Assert.NotNull(fluentValidationResult);
            Assert.False(fluentValidationResult.IsValid);

            var errors = fluentValidationResult.Errors;

            Assert.NotNull(errors);
            Assert.NotEmpty(errors);
            string specificError = errors.FirstOrDefault().PropertyName;

            Assert.Contains(propertyName, specificError, StringComparison.OrdinalIgnoreCase);
        }
    }
}
