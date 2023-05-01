using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.UnitTests.Common.Extensions;

public class StringExtensionsUnitTests
{
    [Theory]
    [InlineData("test1@gmail.com", "test.1@gmail.com")]
    [InlineData("test2@gmail.com", "test2@gmail.com")]
    public void MustNormalizeEmailUser(string expectedEmail, string email)
    {
        //arrange
        var user = new User()
        {
            Email = email
        };

        //act
        var result = user.Email.NormalizeEmail();

        //assert
        Assert.Equal(expectedEmail, result);
    }
}