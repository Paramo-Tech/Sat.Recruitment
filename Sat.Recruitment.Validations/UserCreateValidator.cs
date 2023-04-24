using FluentValidation;
using Sat.Recruitment.DTOs.Enums;
using Sat.Recruitment.DTOs.Requests;
using System;

namespace Sat.Recruitment.Validations
{
    public class UserCreateValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Errors.NameIsRequired);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Errors.EmailIsRequired)
                .EmailAddress();

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(Errors.AddressIsRequired);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(Errors.PhoneIsRequired);

            RuleFor(x => x.UserType)
                .NotNull()
                .WithMessage(Errors.UserTypeIsRequired)
                .Must(x => Enum.IsDefined(typeof(UserType), x))
                .WithMessage(Errors.UserTypeEnum);

            RuleFor(x => x.Money)
                .NotNull()
                .WithMessage(Errors.MoneyIsRequired)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Errors.MoneyGreaterThanZero);
        }
    }
}