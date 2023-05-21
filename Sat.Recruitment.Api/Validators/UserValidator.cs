using Sat.Recruitment.Api.ViewModels;

namespace Sat.Recruitment.Api.Validators
{
    public class UserValidator
    {
        public static bool Validate(UserDTO user, out string errors)
        {
            errors = string.Empty;

            if (string.IsNullOrEmpty(user.Name))
                errors = "The name is required";
            if (string.IsNullOrEmpty(user.Email))
                errors += " The email is required";
            if (string.IsNullOrEmpty(user.Address))
                errors += " The address is required";
            if (string.IsNullOrEmpty(user.Phone))
                errors += " The phone is required";

            return string.IsNullOrEmpty(errors);
        }
    }
}
