using System;

namespace Sat.Recruitment.Common.ParseDecimal
{
    public static class ParseString
    {
        public static decimal TryParseToDecimal(string text)
        {
            try
            {
                return Convert.ToDecimal(text);
            }
            catch
            {

                return 0;
            }
        }
    }
}
