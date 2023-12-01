using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.DTOs.ValidationAttributeHelper
{
    public class ParseableToNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                // Handle the null or empty value scenario
                return ValidationResult.Success;
            }

            if (decimal.TryParse(value.ToString(), out _))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "The value is not parseable to a number.");
            }
        }
    }
}
