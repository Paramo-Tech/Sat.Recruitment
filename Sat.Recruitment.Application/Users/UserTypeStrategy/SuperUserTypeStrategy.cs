using Sat.Recruitment.Application.Users.Commands;

namespace Sat.Recruitment.Application.Users.UserTypeStrategy
{
    public class SuperUserTypeStrategy : IUserTypeStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = money * percentage;
                return money + gif;
            }

            return money;
        }
    }
}
