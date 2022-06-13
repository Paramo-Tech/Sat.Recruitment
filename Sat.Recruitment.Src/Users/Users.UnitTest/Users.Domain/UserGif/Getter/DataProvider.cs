using Users.Domain;
using Users.Domain.UserGif.Calculators;

namespace Users.UnitTest.Users.Domain.UserGif.Getter
{
    public static class DataProvider
    {
        public static IEnumerable<object[]> Calculators()
        {
            yield return new object[] { UserType.Normal, new CalculateNormalUserGif() };
            yield return new object[] { UserType.Premium, new CalculatePremiunUserGif() };
            yield return new object[] { UserType.SuperUser, new CalculateSuperUserGif() };
        }
    }
}
