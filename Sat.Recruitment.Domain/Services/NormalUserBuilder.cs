using System;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Api.Domain.Services
{
    public sealed class NormalUserBuilder : UserBaseBuilder
    {
        private const decimal MaxMoney = 100;
        private const decimal MinMoney = 10;
        private const double PercMaxMoney = 0.12;
        private const double PercMinMoney = 0.8;

        public NormalUserBuilder(IUserModel modelUserModel) : base(modelUserModel)
        {
        }

        public override decimal CalculeMoney()
        {
            var money = ModelUserModel.Money;
            if (money > MaxMoney)
            {
                var percentage = Convert.ToDecimal(PercMaxMoney);
                //If new user is normal and has more than USD100
                var gif = money * percentage;
                return money + gif;
            }

            if (money < MaxMoney)
            {
                if (money > MinMoney)
                {
                    var percentage = Convert.ToDecimal(PercMinMoney);
                    var gif = money * percentage;
                    return money + gif;
                }
            }

            return money;
        }
    }
}