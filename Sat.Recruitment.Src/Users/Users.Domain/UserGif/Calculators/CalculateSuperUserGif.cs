namespace Users.Domain.UserGif.Calculators
{
    public class CalculateSuperUserGif : ICalculateUserGif
    {
        private const decimal MaxLimit = 100;
        private const double PercentageMaxLimit = 0.2;

        public decimal Execute(decimal currentMoney)
        {
            if (currentMoney > MaxLimit)
            {
                return currentMoney * Convert.ToDecimal(PercentageMaxLimit);
            }

            return decimal.Zero;
        }
    }
}
