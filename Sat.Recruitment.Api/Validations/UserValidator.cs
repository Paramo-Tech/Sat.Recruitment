using FluentValidation;
using FluentValidation.Validators;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using System;

namespace Sat.Recruitment.Api.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        private const string ERROR_MESSAGE = "The {PropertyName} is required";

        public UserValidator()
        {
            RuleFor(user => user.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(ERROR_MESSAGE).Length(2, 50);
            RuleFor(user => user.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(ERROR_MESSAGE).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
            RuleFor(user => user.Address).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(ERROR_MESSAGE);
            RuleFor(user => user.Phone).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(ERROR_MESSAGE).Matches(@"^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
            RuleFor(user => user.UserType).Cascade(CascadeMode.Stop).Must( x => Enum.TryParse(x, out UserType result)).WithMessage("The {PropertyName} is invalid");
        }
    }
}
