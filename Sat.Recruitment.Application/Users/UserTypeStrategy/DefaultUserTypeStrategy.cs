namespace Sat.Recruitment.Application.Users.UserTypeStrategy
{
    public class DefaultUserTypeStrategy : IUserTypeStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            return money;
        }
    }
}
