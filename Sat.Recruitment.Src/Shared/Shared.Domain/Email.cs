using Shared.Domain.Exceptions;
using System.Net.Mail;

namespace Shared.Domain
{
    public class Email
    {
        private readonly string value;

        public string Value => value;

        public Email(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("The email can't be empty");
            }

            if (!this.IsValidEmail(value))
            {
                throw new DomainException("Invalid email format");
            }

            this.value = NormalizeEmail(value);
        }

        public override bool Equals(object? other) => other is Email email && value.Equals(email.value);

        public override int GetHashCode() => HashCode.Combine(value);

        private static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);

                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
