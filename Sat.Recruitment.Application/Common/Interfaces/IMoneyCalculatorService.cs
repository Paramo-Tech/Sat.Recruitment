using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Interfaces
{
    public interface IMoneyCalculatorService
    {
        void CalculateMoney(User user);
    }
}
