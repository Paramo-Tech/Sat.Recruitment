using System;

namespace Sat.Recruitment.Common.ParseDecimal
{
    public static class ParseDecimalByUser
    {

        public static decimal MoneyTypeNormal(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.12);

            if (money < 100 && money > 10)
                percentage = Convert.ToDecimal(0.8);

            return money + (money * percentage);
        }

        public static decimal MoneyTypePremium(decimal money)
        {
            decimal gif = 0;
            if (money > 100)
                gif = money * 2;

            return money + gif;
        }

        public static decimal MoneyTypeSuperUser(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.20);

            return money + (money * percentage);
        }

    }
}
