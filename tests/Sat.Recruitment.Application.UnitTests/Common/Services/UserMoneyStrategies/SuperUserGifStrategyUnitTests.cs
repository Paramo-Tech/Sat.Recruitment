using Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.UnitTests.Common.Services.UserMoneyStrategies;

public class SuperUserGifStrategyUnitTests
{
    [Theory]
    [InlineData(100, 100)]
    [InlineData(144, 120)]
    public void Calculate(decimal expected, decimal money)
    {
        //arrange
        var superUserGifStrategy = CreateTarget();

        //act
        var result = superUserGifStrategy.Calculate(new User { Money = money });

        //assert
        Assert.Equal(expected, result);
    }

    private static SuperUserGifStrategy CreateTarget()
    {
        return new SuperUserGifStrategy();
    }

}