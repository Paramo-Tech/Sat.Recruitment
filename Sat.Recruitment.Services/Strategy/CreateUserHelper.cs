using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Strategy
{
    public static class CreateUserHelper
    {
        public static decimal HandleMoneyStrategy(UserTypeEnum userType, decimal money)
        {
            decimal processedMoney = 0;
            BaseStrategy strategy = null;

            switch (userType)
            {
                case UserTypeEnum.NORMAL:
                    strategy = new NormalUserStrategy(money);
                    processedMoney = strategy.ProcessMoney();
                    break;
                case UserTypeEnum.SUPERUSER:
                    strategy = new SuperUserStrategy(money);
                    processedMoney = strategy.ProcessMoney();
                    break;
                case UserTypeEnum.PREMIUM:
                    strategy = new PremiumUserStrategy(money);
                    processedMoney = strategy.ProcessMoney();
                    break;
                default:
                    break;
            }

            return money;
        }

        public static string NormalizeMail(string mail)
        {
            string normalizedMail;
            var aux = mail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            normalizedMail = string.Join("@", new string[] { aux[0], aux[1] });

            return normalizedMail;
        }

    }
}
