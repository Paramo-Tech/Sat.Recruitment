using Core.CQS.Commands;
using FluentValidation;

namespace Infraestructure.Validators
{
    public class CreateNewUserValidator : AbstractValidator<CreateNewUserDTO>
    {
        public CreateNewUserValidator()
        {
            RuleFor(person => person.name).NotEmpty().WithMessage("name is required.");
            RuleFor(person => person.email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("email incorrect format.");
            RuleFor(person => person.phone).NotEmpty().WithMessage("phone is required.");
            RuleFor(person => person.address).NotEmpty().WithMessage("address is required.");
            RuleFor(x => x.money).NotEmpty().Must(BeValidDecimal).WithMessage("invalid money format");
        }

        private bool BeValidDecimal(string value)
        {
            return decimal.TryParse(value, out _);
        }
    }
}
