using FluentValidation;
using Sat.Recruitment.Core.DomainEntities;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Core.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            // Name
            RuleFor(user => user.Name).NotEmpty().WithMessage("The name is required");

            // Email
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("The email is required");

            // Address
            RuleFor(user => user.Address).NotEmpty().WithMessage("The address is required");

            // Phone
            RuleFor(user => user.Phone).NotEmpty().Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage("The phone is required");
        }
    }
}
