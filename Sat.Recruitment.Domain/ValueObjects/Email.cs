using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Sat.Recruitment.Domain.ValueObjects
{
	public class Email: StringValueObject
	{
		public Email(string value): base(value)
		{
			EnsureIsNotEmpty(value);
            value = NormalizeEmail(value);
			EnsureIsAnEmail(value);
		}

        private void EnsureIsAnEmail(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The {nameof(Email)} is required, current value {value}");//TODO: Custom exception
            }
        }

        private void EnsureIsNotEmpty(string value)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(emailPattern);
            var isEmail= regex.IsMatch(value);
            if (!isEmail)
            {
                throw new ArgumentException($"The {nameof(Email)} is required, current value {value}");//TODO: Custom exception
            }
        }

        public static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}

