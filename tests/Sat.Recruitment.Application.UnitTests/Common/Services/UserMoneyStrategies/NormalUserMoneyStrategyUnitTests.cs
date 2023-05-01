using Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.UnitTests.Common.Services.UserMoneyStrategies;

public class NormalUserMoneyStrategyUnitTests
{
    [Theory]
    [InlineData(9, 9)]
    [InlineData(162, 90)]
    [InlineData(134.40, 120)]
    public void Calculate(decimal expected, decimal money)
    {
        //arrange
        var normalUserMoneyStrategy = CreateTarget();

        //act
        var result = normalUserMoneyStrategy.Calculate(new User { Money = money });

        //assert
        Assert.Equal(expected, result);
    }

    private static NormalUserGifStrategy CreateTarget()
    {
        return new NormalUserGifStrategy();
    }
}