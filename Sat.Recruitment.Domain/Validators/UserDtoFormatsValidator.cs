using FluentValidation;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Enum;

namespace Sat.Recruitment.Domain.Validators
{
    public class UserDtoFormatsValidator : AbstractValidator<UserDto>
    {
        public UserDtoFormatsValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.UserType).IsEnumName(typeof(UserTypeEnum), false);

            RuleFor(d => d.Money).Must(ValidateMoneyIsDecimal)
                      .WithErrorCode("Invalid Money type, it has to be decimal.");
        }


        private bool ValidateMoneyIsDecimal(string money)
        {
            var isDecimal = Decimal.TryParse(money, out decimal moneyValue);
            return isDecimal;
             
        }

      
    }
}
