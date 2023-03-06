using Sat.Recruitment.Application.Users.Commands;

namespace Sat.Recruitment.Application.Users.UserTypeStrategy
{
    public interface IUserTypeStrategy
    {
        decimal CalculateGif(decimal money);
    }
}
