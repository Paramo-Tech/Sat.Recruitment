using Sat.Recruitment.Application.Users.Commands;

namespace Sat.Recruitment.Application.Users.UserTypeStrategy
{
    public class NormalUserTypeStrategy : IUserTypeStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                var gif = money * percentage;
                return money + gif;
            }

            if (money < 100 && money > 10)
            {
                var percentage = Convert.ToDecimal(0.8);
                var gif = money * percentage;
                return money + gif;
            }

            return money;
        }
    }
}
