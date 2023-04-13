using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class EmailValidationAttribute : ValidationAttribute
{
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public override bool IsValid(object value)
    {
        if (value == null)
            return true; 

        string email = value.ToString();

        if (string.IsNullOrEmpty(email))
            return true; 

        Regex regex = new Regex(EmailPattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(email);
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The field {name} is not a valid email.";
    }
}




