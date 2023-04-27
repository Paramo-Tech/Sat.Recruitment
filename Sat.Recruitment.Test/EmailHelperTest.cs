using Sat.Recruitment.Application.Services;

namespace Sat.Recruitment.Test
{
    public class EmailHelperTest
    {
        [Theory()]
        [InlineData("test@mail.com", "test@mail.com")]
        [InlineData("test+ddasd@mail.com", "test@mail.com")]
        [InlineData("test.ddasd@mail.com", "testddasd@mail.com")]
        public void NormalizeEmail_MustMatch(string email, string expectedEmail)
        {
            EmailHelper service = new EmailHelper();

            email = service.NormalizeEmail(email);

            Assert.Equal(email, expectedEmail);
        }
    }
}
