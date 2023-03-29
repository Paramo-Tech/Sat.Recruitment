using System;

namespace Sat.Recruitment.Api.Features.Users
{
    public sealed class Email
    {
        private Email(string value) => Value = value;

        public string Value { get; }

        public static Email Create(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            var normalizedEmail = string.Join("@", new string[] { aux[0], aux[1] });

            return new Email(normalizedEmail);
        }

        public static explicit operator Email(string email) => Create(email);

        public static bool operator ==(Email a, Email b)
        {
            if (a is null && b is null) return true;

            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Email a, Email b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if (!(obj is Email other)) return false;

            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
