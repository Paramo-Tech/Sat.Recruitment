using FluentValidation;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Domain.Forms;

namespace Sat.Recruitment.Api.Validators
{
    public class AuthValidator : AbstractValidator<LoginForm>
    {
        public AuthValidator()
        {

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Messages.PasswordErrorRequired);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Messages.EmailErrorRequired);

        }
    }
}
