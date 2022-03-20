using FluentValidation;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Api.Validators
{
    public class UsersValidator : AbstractValidator<UserDto>
    {
        public UsersValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Messages.NameErrorRequired);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Messages.EmailErrorRequired);

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(Messages.AddressErrorRequired);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(Messages.PhoneErrorRequired);

        }
    }
}
