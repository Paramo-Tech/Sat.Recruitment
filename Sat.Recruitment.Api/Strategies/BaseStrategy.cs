namespace Sat.Recruitment.Api.Strategies
{
    public class BaseStrategy
    {
        public decimal CalculateGift(decimal money, decimal percentaje)
        {
            return money * percentaje;
        }
    }
}
