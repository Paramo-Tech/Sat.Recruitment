using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Business;

using System.Text.RegularExpressions;
using Sat.Recruitment.Common;

namespace Sat.Recruitment.Business.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(AppConstants.Validations.NAME_REQ); ;
            RuleFor(user => user.Address).NotEmpty().WithMessage(AppConstants.Validations.ADDRESS_REQ);
            RuleFor(user => user.Phone).NotEmpty().WithMessage(AppConstants.Validations.PHONE_REQ);
            RuleFor(user => user.Email).NotEmpty().WithMessage(AppConstants.Validations.EMAIL_REQ);
            RuleFor(user => user.Money).GreaterThan(-1).WithMessage(AppConstants.Validations.MONEY_AMOUNTT_INV);
            RuleFor(user => user.Email).Must(Email => Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")).WithMessage(AppConstants.Validations.EMAIL_INV);

        }

    }
}
