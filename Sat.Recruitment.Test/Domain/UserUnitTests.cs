using Sat.Recruitment.Domain.Exceptions;
using Sat.Recruitment.Domain.Model;
using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    public class UserUnitTests
    {
        [Fact]
        public void SetPhone_WithValidNumber_SetsPhone()
        {
            // Arrange. 
            var user = new User();
            var phoneNumber = "+5493446381537";
            
            // Act.
            user.SetPhone(phoneNumber);

            // Assert.
            Assert.Equal(phoneNumber, user.Phone);
        }

        [Fact]
        public void SetPhone_WithInvalidNumber_ThrowsException()
        {
            // Arrange. 
            var user = new User();
            var phoneNumber = "Not a number!";

            // Act.
            void setPhone () => user.SetPhone(phoneNumber);

            // Assert.
            Assert.Throws<DomainException>(setPhone);
        }

        [Fact]
        public void SetEmail_WithValidEmailAddress_SetsEmail()
        {
            // Arrange. 
            var user = new User();
            var email = "johndoe@fake.domain.com";

            // Act.
            user.SetEmail(email);

            // Assert.
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void SetEmail_WithValidEmailAddress_ThrowsException()
        {
            // Arrange. 
            var user = new User();
            var email = "fakeema!#il@/&%$%,.12.com";

            // Act.
            void setEmail() => user.SetEmail(email);

            // Assert.
            Assert.Throws<DomainException>(setEmail);
        }
    }
}
