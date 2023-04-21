using FluentValidation;
using FluentValidation.Results;
using Sat.Recruitment.Token.Models.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Token.Models.Validators
{
    public class AccountDtoValidator :  AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("El nombre de usuario es requerido.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Ingrese una clave válida");
        }

        public List<string> ConvertFailuresToErrorMessages(List<ValidationFailure> validationFailures)
        {
            return validationFailures
                .Select(validationFailure => validationFailure.ErrorMessage)
                .ToList();
        }
    }
}
