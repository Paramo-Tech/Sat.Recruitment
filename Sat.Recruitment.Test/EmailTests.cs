using Sat.Recruitment.Api.Features.Users;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class EmailTests
    {
        [Theory]
        [InlineData("homer+456789@compuglobalhypermeganet.com", "homer@compuglobalhypermeganet.com")]
        [InlineData("homer.simpson@compuglobalhypermeganet.com", "homersimpson@compuglobalhypermeganet.com")]
        public void InnerValueInEmailShouldBeNormalized(string emailValue, string emailNormalized)
        {
            var email = Email.Create(emailValue);

            Assert.Equal(emailNormalized, email.Value);
        }

        [Theory]
        [InlineData("homer@compuglobalhypermeganet.com", "homer@compuglobalhypermeganet.com", true)]
        [InlineData("homer@compuglobalhypermeganet.com", "HOMER@COMPUGLOBALHYPERMEGANET.COM", true)]
        [InlineData("homer@compuglobalhypermeganet.com", "marge@simpsons.com", false)]
        public void EmailEqualityShouldWorksCorrectly(string emailA, string emailB, bool expectedEquality)
        {
            var email1 = Email.Create(emailA);
            var email2 = Email.Create(emailB);

            var equalsResult = email1.Equals(email2);
            var operatorResult = email1 == email2;

            Assert.Equal(expectedEquality, equalsResult);
            Assert.Equal(expectedEquality, operatorResult);
        }
    }
}
