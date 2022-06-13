using Users.Domain;
using Users.Domain.UserGif.Calculators;
using Users.Domain.UserGif.Getter;
using Xunit;

namespace Users.UnitTest.Users.Domain.UserGif.Getter
{
    public class GifCalculateGetterTest
    {
        private readonly GifCalculateGetter gifCalculateGetter = new();

        [Theory]
        [MemberData(nameof(DataProvider.Calculators), MemberType = typeof(DataProvider))]
        public void Handle_WhenCommandIsInvalid_ExceptionExpected(
            UserType userType,
            ICalculateUserGif expectedCalculateUserGif)
        {
            var result = this.gifCalculateGetter.GetCalculator(userType);

            Assert.IsType(expectedCalculateUserGif.GetType(), result);
        }
    }
}
