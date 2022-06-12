using Shared.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Shared.Domain
{
    public class Phone
    {
        private readonly string value;

        private const string phoneRegex = @"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$";

        public string Value => value;

        public Phone(string value)
        {
            this.ValidateFormat(value);

            this.value = value;
        }

        public override bool Equals(object? other) => other is Phone phone && value.Equals(phone.value);

        public override int GetHashCode() => HashCode.Combine(value);

        private void ValidateFormat(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new DomainException("The phone can't be empty");
            }

            if (Regex.IsMatch(phone, phoneRegex))
            {
                throw new DomainException("Invalid phone number format");
            }         
        }
    }
}
