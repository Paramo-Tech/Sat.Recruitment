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
        }
    }
}
