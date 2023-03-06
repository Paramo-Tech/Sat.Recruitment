using Sat.Recruitment.Application.Users.Commands;

namespace Sat.Recruitment.Application.Users.UserTypeStrategy
{
    public class PremiumUserTypeStrategy : IUserTypeStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                var gif = money * 2;
                return money + gif;
            }

            return money;
        }
    }
}
