using FluentValidation;
using Sat.Recruitment.Api.Models.DTO;

namespace Sat.Recruitment.Api.Models.Validators
{

    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Phone)
                .NotNull().NotEmpty().WithMessage("Phone is required");
            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Address)
                .NotNull().NotEmpty().WithMessage("Address is required");
        }
    }
}
