using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private const string EmailRegex = @"^[A-Za-z0-9+._-]{2,}[@][A-Za-z0-9._-]{2,}[.][A-Za-z0-9._-]{2,}$";

        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The email is required").Matches(EmailRegex).WithMessage("The email is not valid");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The address is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("The phone is required");
            RuleFor(x => x.Money).GreaterThanOrEqualTo(0).WithMessage("The amount of money cannot be less than zero");
        }
    }
}
