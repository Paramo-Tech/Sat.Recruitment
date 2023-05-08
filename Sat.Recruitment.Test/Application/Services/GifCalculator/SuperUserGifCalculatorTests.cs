using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Xunit;

namespace Sat.Recruitment.Test.Application.Services.GifCalculator
{
    public class SuperUserGifCalculatorTests
    {
        [Theory]
        [InlineData(99, 99)]
        [InlineData(100.5, 301.5)]
        [InlineData(300, 900)]
        public void CalculateGif_WithValidInput_ReturnsMoney(decimal money, decimal expectedResult)
        {
            // Arrange.
            var calculator = new SuperUserGifCalculator();

            // Act.
            var result = calculator.Calculate(money);

            // Assert.
            Assert.Equal(expectedResult, result);
        }
    }
}
