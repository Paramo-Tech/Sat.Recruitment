using FluentValidation;

namespace Sat.Recruitment.Business.User.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.name).NotEmpty().WithMessage("The name is required");
            RuleFor(u => u.email).NotEmpty().WithMessage("The email is required");
            RuleFor(u => u.address).NotEmpty().WithMessage("The address is required");
            RuleFor(u => u.phone).NotEmpty().WithMessage("The phone is required");
        }
    }
}
