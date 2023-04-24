using Sat.Recruitment.DTOs.Enums;

namespace Sat.Recruitment.Services.Domain.MoneyIncrease
{
    public interface ICalculateMoneyIncreaseFactory
    {
        ICalculateMoneyIncrease GetInstance(UserType userType);
    }
}