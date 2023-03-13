using System;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Domain.ValueObjects
{
	public class Phone:StringValueObject
	{
		public Phone(string value): base(value)
        {
            EnsureIsNotEmpty(value);
            EnsureIsAPhoneNumber(value);
        }

        private void EnsureIsNotEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The {nameof(Phone)} is required, current value {value}");//TODO: Custom exception
            }
        }

        private void EnsureIsAPhoneNumber(string value)
        {
            Regex regex = new Regex(@"^\+(?:[0-9]-?){6,14}[0-9]$");
            bool isValidPhoneNumber = regex.IsMatch(value);

            if (!isValidPhoneNumber)
            {
                throw new ArgumentException($"The {nameof(Phone)} is required, current value {value}");//TODO: Custom exception
            }
        }
    }
}

