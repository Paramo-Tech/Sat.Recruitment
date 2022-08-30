using System;

namespace Sat.Recruitment.Api.Utilities
{
    public class Util : IUtil
    {
        public decimal MoneyTypeNormal(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.12);

            if (money < 100 && money > 10)
                percentage = Convert.ToDecimal(0.8);

            return money + (money * percentage);
        }

        public decimal MoneyTypePremium(decimal money)
        {
            decimal gif = 0;
            if (money > 100)
                gif = money * 2;
            
            return money + gif;
        }

        public decimal MoneyTypeSuperUser(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.20);

            return money + (money * percentage);
        }

        public string NormaliceEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
