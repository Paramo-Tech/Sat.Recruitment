using FluentValidation;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Commands;

namespace Sat.Recruitment.Api.Services.Validations
{
    public class CreateUserValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.name).NotEmpty().WithMessage("The name is required");
            RuleFor(u => u.email).NotEmpty().WithMessage("The email is required");
            RuleFor(u => u.email).EmailAddress().WithMessage("The email has invalid format");
            RuleFor(u => u.address).NotEmpty().WithMessage("The address is required");
            RuleFor(u => u.phone).NotEmpty().WithMessage("The phone is required");
        }
    }
}
