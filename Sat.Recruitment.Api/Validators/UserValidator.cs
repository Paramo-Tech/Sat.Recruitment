using FluentValidation;
using Sat.Recruitment.Application.Dto;

namespace Sat.Recruitment.Api.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Name} is required.");
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty().WithMessage("{Email} is required.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{Address} is required.");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("{Phone} is required.");


        }

    }
}
