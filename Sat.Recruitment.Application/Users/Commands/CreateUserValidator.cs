using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(c => c.Email).EmailAddress().WithMessage("The email is required");
            RuleFor(c => c.Address).NotEmpty().WithMessage("The address is required");
            RuleFor(c => c.Phone).NotEmpty().WithMessage("The phone is required");
        }
    }
}
