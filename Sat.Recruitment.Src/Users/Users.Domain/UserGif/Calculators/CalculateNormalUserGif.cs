namespace Users.Domain.UserGif.Calculators
{
    public class CalculateNormalUserGif : ICalculateUserGif
    {
        private const decimal MaxLimit = 100;
        private const decimal MinLimit = 10;
        private const double PercentageMaxLimit = 0.12;
        private const double PercentageMinLimit = 0.8;

        public decimal Execute(decimal currentMoney)
        {
            if (currentMoney > MaxLimit)
            {
                return currentMoney * Convert.ToDecimal(PercentageMaxLimit);
            }

            if (currentMoney < MaxLimit && currentMoney > MinLimit)
            {
                return currentMoney * Convert.ToDecimal(PercentageMinLimit);
            }

            return decimal.Zero;
        }
    }
}
