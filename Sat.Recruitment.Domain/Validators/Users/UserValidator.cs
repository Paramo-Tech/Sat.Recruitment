using FluentValidation;
using Sat.Recruitment.Domain.Models.Users;

namespace Sat.Recruitment.Domain.Validators.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().NotNull().WithMessage("The name is required");
            RuleFor(u => u.Email).NotEmpty().NotNull().WithMessage("The email is required");
            RuleFor(u => u.Address).NotEmpty().NotNull().WithMessage("The address is required");
            RuleFor(u => u.Phone).NotEmpty().NotNull().WithMessage("The phone is required");
            RuleFor(e => e.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Invalid mail format");
        }
    }
}
