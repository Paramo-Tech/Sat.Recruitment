using Domain;
using System;
using System.Collections.Generic;

namespace Application
{
    public class FactoryMoneyUser
    {
        private readonly Dictionary<string, IUserPercentCalculate> users;

        public FactoryMoneyUser()
        {
            users = new Dictionary<string, IUserPercentCalculate>
            {
                { "Normal", new NormalUserPercentCalculate() },
                { "SuperUser", new SuperUserPercentCalculate() },
                { "Premium", new PremiumUserPercentCalculate() }
            };
        }

        public decimal GetMoneyCalculatedByUser(User user)
        {
            var percentage = users[user.UserType].GetPercentage(user.Money);
            var gif = user.Money * Convert.ToDecimal(percentage);
            return user.Money + gif;
        }
    }
}