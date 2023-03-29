using Sat.Recruitment.Api.Features.Users;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UsersTests
    {
        [Theory]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 10, 10)]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 50, 90)]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 150, 168)]
        public void GiftForNormalUserShouldBeCalculatedCorrectly(
            string name, string email, string address, string phone, decimal money, decimal moneyPlusGift)
        {
            var normalUser = new Normal(name, (Email)email, address, phone, money);

            Assert.Equal(moneyPlusGift, normalUser.Money);
        }

        [Theory]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 10, 10)]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 150, 180)]
        public void GiftForSuperUserShouldBeCalculatedCorrectly(
            string name, string email, string address, string phone, decimal money, decimal moneyPlusGift)
        {
            var superUser = new SuperUser(name, (Email)email, address, phone, money);

            Assert.Equal(moneyPlusGift, superUser.Money);
        }

        [Theory]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 10, 10)]
        [InlineData(
            "Homer Simpson", "homer@compuglobalhypermeganet.com", "742 Evergreen Terrace", "+15555555555", 150, 300)]
        public void GiftForPremiumUserShouldBeCalculatedCorrectly(
            string name, string email, string address, string phone, decimal money, decimal moneyPlusGift)
        {
            var premiumUser = new Premium(name, (Email)email, address, phone, money);

            Assert.Equal(moneyPlusGift, premiumUser.Money);
        }
    }
}
