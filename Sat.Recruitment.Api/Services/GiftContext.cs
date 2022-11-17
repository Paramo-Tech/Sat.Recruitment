namespace Sat.Recruitment.Api.Services
{
    public class GiftContext
    {
        private IGift gift;
        
        public decimal GetPercentaje(string type, decimal money)
        {
            decimal percentaje = 0;
            switch (type)
            {
                case "Normal":
                    gift = new GiftNormalUser();
                    break;
                case "SuperUser":
                    gift = new GiftSuperUser();
                    break;
                case "Premium":
                    gift = new GiftPremiumUser();
                    break;
            }
            if(gift != null) 
                percentaje = gift.CalcGift(money);

            return money*percentaje;
        }

    }
}
