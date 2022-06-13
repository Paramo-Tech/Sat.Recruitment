namespace Users.Domain.UserGif.Calculators
{
    public class CalculateNormalUserGif : ICalculateUserGif
    {
        public decimal Execute(decimal currentMoney)
        {
            if (currentMoney > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                return currentMoney * percentage;
            }

            if (currentMoney < 100 && currentMoney > 10)
            {
                var percentage = Convert.ToDecimal(0.8);

                return currentMoney * percentage;
            }

            return decimal.Zero;
        }
    }
}
