using Sat.Recruitment.Api.Logic.Interface;
using System;

namespace Sat.Recruitment.Api.Logic
{
    public class Helpers : Ihelpers
    {
        public string normalizeEmail(string email)
        {
            try
            {
                var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
                return string.Join("@", new string[] { aux[0], aux[1] });
            }
            catch
            {
                throw new InvalidOperationException("Invalid email");
            }
        }

        public decimal convertMoney(decimal money, decimal percentage)
        {
            decimal gif = money * percentage;
            money = money + gif;
            return money;
        }
    }
}
