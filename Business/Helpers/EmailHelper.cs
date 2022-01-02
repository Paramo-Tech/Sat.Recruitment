using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Business.Helpers
{
    public static class EmailHelper
    {
        public static string Normalize(string email)
        {
            if (string.IsNullOrEmpty(email))
                return "";

            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }   
    }
}
