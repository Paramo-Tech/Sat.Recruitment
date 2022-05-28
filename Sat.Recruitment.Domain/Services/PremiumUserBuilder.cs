using Sat.Recruitment.Api.Domain.Services.Contracts;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Api.Domain.Services
{
    public sealed class PremiumUserBuilder : UserBaseBuilder
    {
        private const decimal MinMoney = 100;
        private const double Coefficient = 0.8;

        public PremiumUserBuilder(IUserModel modelUserModel) : base(modelUserModel)
        {
        }

        public override decimal CalculeMoney()
        {
            var money = ModelUserModel.Money;
            if (money > MinMoney)
            {
                var gif = money * (decimal) Coefficient;
                return money + gif;
            }


            return money;
        }
    }
}