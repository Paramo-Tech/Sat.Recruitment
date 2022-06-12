namespace Users.Domain.UserGif
{
    public class CalculateUserGif : ICalculateUserGif
    {
        public decimal Execute(UserType userType, decimal currentMoney)
        {
            var gif = decimal.Zero;
            if (UserType.Normal.Equals(userType))
            {
                if (currentMoney > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    gif = currentMoney * percentage;
                }
                if (currentMoney < 100)
                {
                    if (currentMoney > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        gif = currentMoney * percentage;
                    }
                }
            }
            if (UserType.SuperUser.Equals(userType))
            {
                if (currentMoney > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    gif = currentMoney * percentage;
                }
            }
            if (UserType.Premium.Equals(userType))
            {
                if (currentMoney > 100)
                {
                    gif = currentMoney * 2;
                }
            }

            return gif;
        }
    }
}
