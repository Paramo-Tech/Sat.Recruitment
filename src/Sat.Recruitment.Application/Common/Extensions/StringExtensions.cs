using System;
using System.Net.Mail;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static UserTypes ToUserTypes(this string value)
        {
            return value switch
            {
                nameof(UserTypes.Normal) => UserTypes.Normal,
                nameof(UserTypes.SuperUser) => UserTypes.SuperUser,
                nameof(UserTypes.Premium) => UserTypes.Premium,
                _ => UserTypes.None,
            };
        }

        public static string NormalizeEmail(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (IsValidEmail(value))
            {
                string[] parts = value.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                string user = parts[0].Trim();
                string domain = parts[1].Trim();

                user = user.Replace(".", string.Empty, StringComparison.Ordinal).Replace("+", string.Empty, StringComparison.Ordinal);

                return $"{user}@{domain}";
            }
            else
            {
                throw new ArgumentException($"{value} is not a valid email");
            }
        }

        public static bool IsValidEmail(this string value)
        {
            try
            {
                var email = new MailAddress(value);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
