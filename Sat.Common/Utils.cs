using System;

namespace Sat.Common
{
    public static class Utils
    {
        public static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        public static decimal ToDecimal(this string text)
        {
            try
            {
                return Convert.ToDecimal(text);
            }
            catch
            {

                return 0M;
            }
        }
    }
}
