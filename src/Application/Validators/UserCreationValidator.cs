using Application.Contracts.Validators;
using Application.Models;
using FluentValidation;

namespace Application.Validators
{
    public class UserCreationValidator : AbstractValidator<UserCreationDto>, IUserCreationValidator
    {
        public UserCreationValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("The name is required");
            RuleFor(x => x.Email).NotNull().WithMessage("The email is required");
            RuleFor(x => x.Address).NotNull().WithMessage("The address is required");
            RuleFor(x => x.Phone).NotNull().WithMessage("The phone is required");
        }
    }
}