using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Xunit;

namespace Sat.Recruitment.Test.Application.Services.GifCalculator
{
    public class NormalUserGifCalculatorTests
    {
        [Theory]
        [InlineData(9, 9)]
        [InlineData(10.50, 18.9)]
        [InlineData(95, 171)]
        [InlineData(100.50, 112.56)]
        [InlineData(300, 336)]
        public void CalculateGif_WithValidInput_ReturnsMoney(decimal money, decimal expectedResult)
        {
            // Arrange.
            var calculator = new NormalUserGifCalculator();

            // Act.
            var result = calculator.Calculate(money);

            // Assert.
            Assert.Equal(expectedResult, result);
        }
    }
}
