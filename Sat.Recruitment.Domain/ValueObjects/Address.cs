using System;
using System.Xml.Linq;

namespace Sat.Recruitment.Domain.ValueObjects
{
	public class Address: StringValueObject
	{
		public Address(string value):base(value)
        {
            EnsureNotEmpy(value);
        }

        private void EnsureNotEmpy(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The {nameof(Address)} is required, current value {value}");//TODO: Custom exception
            }
        }
    }
}

