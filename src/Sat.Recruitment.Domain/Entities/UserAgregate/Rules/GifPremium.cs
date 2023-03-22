namespace Sat.Recruitment.Domain.Entities.UserAgregate.Rules
{
    public class GifPremium : IGifStrategy
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                return money * 2;
            }
            return 0;
        }
    }
}