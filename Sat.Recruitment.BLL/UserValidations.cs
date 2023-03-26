using System;

namespace Sat.Recruitment.BLL
{
    public class UserValidations
    {
        public static decimal GetUserTypeMoney(string userType, string moneyAmount, decimal newUserMoney)
        {
            decimal bonusAmount = 0;

            decimal money = decimal.Parse(moneyAmount);

            if (userType == "Normal")
            {
                bonusAmount = GetNormalUserBonusAmount(money);
            }
            else if (userType == "SuperUser")
            {
                bonusAmount = GetSuperUserBonusAmount(money);
            }
            else if (userType == "Premium")
            {
                bonusAmount = GetPremiumUserBonusAmount(money);
            }

            return newUserMoney + bonusAmount;
        }

        public static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        private static decimal GetNormalUserBonusAmount(decimal moneyAmount)
        {
            if (moneyAmount > 100)
            {
                decimal percentage = 0.12m;
                return moneyAmount * percentage;
            }
            else if (moneyAmount > 10)
            {
                decimal percentage = 0.8m;
                return moneyAmount * percentage;
            }

            return 0;
        }

        private static decimal GetSuperUserBonusAmount(decimal moneyAmount)
        {
            if (moneyAmount > 100)
            {
                decimal percentage = 0.20m;
                return moneyAmount * percentage;
            }

            return 0;
        }

        private static decimal GetPremiumUserBonusAmount(decimal moneyAmount)
        {
            if (moneyAmount > 100)
            {
                return moneyAmount * 2;
            }

            return 0;
        }
    }
}
