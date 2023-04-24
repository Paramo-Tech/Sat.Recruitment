using Sat.Recruitment.DTOs.Enums;

namespace Sat.Recruitment.Services.Commands
{
    public interface ICalculateMoneyIncreaseCommand
    {
        double Execute(UserType userType, double money);
    }
}
