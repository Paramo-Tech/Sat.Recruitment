using FluentValidation;
using Infraestructure.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Api.Utility
{
    public class Validations : AbstractValidator<UserDto>
    {
        public Validations()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.UserType).NotEmpty();
            RuleFor(x => x.Money).NotEmpty();
        }
    }
}
