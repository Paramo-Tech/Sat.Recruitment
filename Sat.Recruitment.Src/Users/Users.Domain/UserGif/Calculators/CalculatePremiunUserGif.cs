namespace Users.Domain.UserGif.Calculators
{
    public class CalculatePremiunUserGif : ICalculateUserGif
    {
        private const decimal MaxLimit = 100;
        private const double PercentageMaxLimit = 2;

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
