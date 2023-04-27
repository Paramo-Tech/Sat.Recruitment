using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Application.Common.Validators
{
    public class UserTypeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userType = (UserTypes)value;

            var userTypes = Enum.GetValues(typeof(UserTypes)).Cast<UserTypes>();
            userTypes = userTypes.Where(x => x != UserTypes.None);

            return !userTypes.Contains(userType) ? new ValidationResult("The UserType is invalid.") : ValidationResult.Success;
        }
    }
}
