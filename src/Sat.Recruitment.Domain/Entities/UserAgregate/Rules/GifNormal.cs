namespace Sat.Recruitment.Domain.Entities.UserAgregate.Rules
{
    public class GifNormal : IGifStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                return money * 0.12m;
            }
            else if (money > 10)
            {
                return money * 0.8m;
            }
            return 0;
        }
    }
}