using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Handlers
{
    internal static class EmailHandler
    {
        internal static string NormalizeEmail(string email)
        {
            var emailSplit = email.Split('@', StringSplitOptions.RemoveEmptyEntries);
            var emailUser = emailSplit[0];
            var emailDomain = emailSplit[1];

            var atIndex = emailUser.IndexOf("+", StringComparison.Ordinal);
            if (atIndex >= 0)
                emailUser = emailUser.Remove(atIndex);

            emailUser = emailUser.Replace(".", "");

            return string.Join("@", emailUser, emailDomain);
        }
    }
}
