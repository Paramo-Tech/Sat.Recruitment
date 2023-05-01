using FluentValidation;

namespace Sat.Recruitment.Application.Common.Extensions
{
    public static class ValidationExceptionExtensions
    {
        public static string ToErrorMessage(this ValidationException ex)
        {
            var errors = ex.Errors.Select(x => x.ErrorMessage);
            var errorMessage = string.Join(", ", errors);

            return errorMessage;
        }
    }
}
