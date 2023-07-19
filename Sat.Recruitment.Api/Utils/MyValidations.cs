using System.Collections.Generic;
using System.Text;
namespace Sat.Recruitment.Api.Utils{
    public class MyValidations
    {
        public string ValidateErrors(string name, string email, string address, string phone)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(name))
                errors.Add("The name is required");
            
            if (string.IsNullOrEmpty(email))
                errors.Add("The email is required");

            if (string.IsNullOrEmpty(address))
                errors.Add("The address is required");

            if (string.IsNullOrEmpty(phone))
                errors.Add("The phone is required");

            return string.Join(". ", errors);
        }
    }
}
