using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Xunit;

namespace Sat.Recruitment.Test.Application.Services.GifCalculator
{
    public class PremiumUserGifCalculatorTests
    {
        [Theory]
        [InlineData(99, 99)]
        [InlineData(100.50, 120.6)]
        [InlineData(300, 360)]
        public void CalculateGif_WithValidInput_ReturnsMoney(decimal money, decimal expectedResult)
        {
            // Arrange.
            var calculator = new PremiumUserGifCalculator();

            // Act.
            var result = calculator.Calculate(money);

            // Assert.
            Assert.Equal(expectedResult, result);
        }
    }
}
