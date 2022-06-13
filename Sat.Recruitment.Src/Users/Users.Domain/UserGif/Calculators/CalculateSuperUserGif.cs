namespace Users.Domain.UserGif.Calculators
{
    public class CalculateSuperUserGif : ICalculateUserGif
    {
        public decimal Execute(decimal currentMoney)
        {
            if (currentMoney > 100)
            {
                var percentage = Convert.ToDecimal(0.20);

                return currentMoney * percentage;
            }

            return decimal.Zero;
        }
    }
}
