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
    }
}

