using FluentValidation;
using Sat.Recruitment.Core.DTOs;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(post => post.Name)
                .NotNull()
                .Length(2, 200)
                .WithMessage("The name is required");

            RuleFor(post => post.Email)
                .NotNull()
                .WithMessage("The email is required")
                .EmailAddress()
                .WithMessage("The email format is invalid");

            RuleFor(post => post.Address)
               .NotNull()
               .Length(5, 200)
               .WithMessage("The address is required");

            RuleFor(post => post.Phone)
               .NotNull()
               .Length(5, 200)
               .WithMessage("The phone is required")
               .Matches(new Regex(@"^(\+\d{1,3}\s?)?(\d{1,4}\s?)?(\d{1,3}\s?)?(\d{1,4})$"))
               .WithMessage("The phone format is invalid");

        }
    }
}
