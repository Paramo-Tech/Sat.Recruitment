using FluentValidation.Results;

namespace Sat.Recruitment.Application.Common.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public override string Message
        {
            get
            {
                return string.Join(" ", Errors.Select(x => string.Join(" ", x.Value)));
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}