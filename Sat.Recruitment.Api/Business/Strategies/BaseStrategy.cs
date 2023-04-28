namespace Sat.Recruitment.Api.Business.Strategies
{
    public class BaseStrategy
    {
        public decimal CalculateGift(decimal money, decimal percentaje)
        {
            return money * percentaje;
        }
    }
}
