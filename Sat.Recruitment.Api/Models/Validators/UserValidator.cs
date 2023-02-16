using FluentValidation;
using Sat.Recruitment.Api.Models.DTO;

namespace Sat.Recruitment.Api.Models.Validators
{

    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required");
            RuleFor(x => x.Phone)
                .NotNull().WithMessage("Phone is required");
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required");
            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required");
        }
    }
}
