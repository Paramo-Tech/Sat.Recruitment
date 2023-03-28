using FluentValidation;

namespace Sat.Recruitment.Kernel.Features.Users.CreateUserCommand
{
    /// <summary>
    /// Validator for new users
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        private string[] UserTypeValidation = { "Normal", "SuperUser", "Premium"};

        public CreateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Money)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Phone)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.UserType)
                .NotNull()
                .Must(ut => UserTypeValidation.Contains(ut)).WithMessage("'UserType' must be: " + string.Join(", ", UserTypeValidation));
        }
    }
}
