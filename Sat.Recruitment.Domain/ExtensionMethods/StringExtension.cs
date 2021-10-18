using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.ExtensionMethods
{
      public static class StringExtension
    {
        public static string NormalizeEmail(this string value)
        {
            var aux = value.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

           return  string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
