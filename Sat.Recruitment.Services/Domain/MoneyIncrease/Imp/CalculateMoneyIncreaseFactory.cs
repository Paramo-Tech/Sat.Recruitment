using Sat.Recruitment.DTOs.Enums;
using System;

namespace Sat.Recruitment.Services.Domain.MoneyIncrease.Imp
{
    public class CalculateMoneyIncreaseFactory : ICalculateMoneyIncreaseFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CalculateMoneyIncreaseFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public ICalculateMoneyIncrease GetInstance(UserType userType)
        {
            return userType switch
            {
                UserType.Normal => (ICalculateMoneyIncrease)serviceProvider.GetService(typeof(CalculateMoneyIncreaseNormal)),
                UserType.SuperUser => (ICalculateMoneyIncrease)serviceProvider.GetService(typeof(CalculateMoneyIncreaseSuperUser)),
                UserType.Premium => (ICalculateMoneyIncrease)serviceProvider.GetService(typeof(CalculateMoneyIncreasePremium)),
                _ => throw new NotImplementedException(nameof(UserType)),
            };
        }
    }
}