namespace Sat.Recruitment.Domain.Entities.UserAgregate.Rules
{
    public class GifSuperUser : IGifStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                return money * 0.2m;
            }
            return 0;
        }
    }
}