using FluentValidation;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Api.Validators
{
    public class UsersValidator : AbstractValidator<UserCreationForm>
    {
        public UsersValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Messages.NameErrorRequired);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Messages.PasswordErrorRequired);

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
