namespace Users.Domain.UserGif.Calculators
{
    public class CalculatePremiunUserGif : ICalculateUserGif
    {
        public decimal Execute(decimal currentMoney)
        {
            if (currentMoney > 100)
            {
                return currentMoney * 2;
            }

            return decimal.Zero;
        }
    }
}
