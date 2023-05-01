using Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.UnitTests.Common.Services.UserMoneyStrategies;

public class PremiumUserGifStrategyUnitTests
{
    [Theory]
    [InlineData(100, 100)]
    [InlineData(360, 120)]
    public void Calculate(decimal expected, decimal money)
    {
        //arrange
        var premiumUserGifStrategy = CreateTarget();

        //act
        var result = premiumUserGifStrategy.Calculate(new User { Money = money });

        //assert
        Assert.Equal(expected, result);
    }

    private static PremiumUserGifStrategy CreateTarget()
    {
        return new PremiumUserGifStrategy();
    }
}