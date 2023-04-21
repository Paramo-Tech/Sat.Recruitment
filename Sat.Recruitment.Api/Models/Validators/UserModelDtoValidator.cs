using FluentValidation;
using FluentValidation.Results;
using Sat.Recruitment.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Models.Validators
{
    public class UserModelDtoValidator : AbstractValidator<UserModelDto>
    {
        public UserModelDtoValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(user => user.Email).NotEmpty().WithMessage("The email is required").EmailAddress();
            RuleFor(user => user.Address).NotEmpty().WithMessage("The address is required");
            RuleFor(user => user.Phone).NotEmpty().WithMessage("The phone is required").MaximumLength(15);
        }

        public List<string> ConvertFailuresToErrorMessages(List<ValidationFailure> validationFailures)
        {
            return validationFailures
                .Select(validationFailure => validationFailure.ErrorMessage)
                .ToList();
        }
    }
}
