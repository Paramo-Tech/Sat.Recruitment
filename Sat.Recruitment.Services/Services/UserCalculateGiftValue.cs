using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Services.Services
{
    /// <summary>
    /// Implementation to calculate gift value per user
    /// </summary>
    public class UserCalculateGiftValue : IUserCalculateGiftValue
    {
        /// <summary>
        /// Return gift value acording key and money
        /// </summary>
        /// <param name="key">Can be Normal, SuperUser, Premium</param>
        /// <param name="money">Money value</param>
        /// <returns>Gift value</returns>
        public decimal GetGiftValue(string key, decimal money)
        {
            decimal value = 0;
            decimal percentage = 0;
            switch (key)
            {
                case "Normal":
                    if (money > 100)
                        percentage = Convert.ToDecimal(0.12);
                    if (money < 100 && money > 10)
                        percentage = Convert.ToDecimal(0.8);
                    break;
                case "SuperUser":
                    if (money > 100)
                        percentage = Convert.ToDecimal(0.20);
                    break;
                case "Premium":
                    if (money > 100)
                        percentage = Convert.ToDecimal(2);
                    break;
                default:
                    percentage = 0;
                    break;
            }
            value = money * percentage;
            return value;
        }
    }
}
