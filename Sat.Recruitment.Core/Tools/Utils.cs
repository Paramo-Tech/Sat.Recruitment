using System;

namespace Sat.Recruitment.Core.Tools
{
    static  class Utils
    {
        public static string NormalizeEmail(string inputEmail)
        {
            // Normalize email
            var aux = inputEmail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
