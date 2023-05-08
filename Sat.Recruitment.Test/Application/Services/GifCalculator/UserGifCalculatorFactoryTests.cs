using Sat.Recruitment.Application.Services.GifCalculator.Factory;
using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Sat.Recruitment.Domain.Enum;
using System;
using Xunit;

namespace Sat.Recruitment.Test.Application.Services.GifCalculator
{
    public class UserGifCalculatorFactoryTests
    {
        [Theory]
        [InlineData(UserType.Normal, typeof(NormalUserGifCalculator))]
        [InlineData(UserType.Premium, typeof(PremiumUserGifCalculator))]
        [InlineData(UserType.SuperUser, typeof(SuperUserGifCalculator))]
        public void CreateCalculator_WithValidType_ReturnsAppropiateStrategy(UserType userType, Type typeReturned)
        {
            // Arrange.
            var factory = new UserGifCalculatorFactory();

            // Act.
            var calculatorStrategy = factory.CreateCalculator(userType);

            // Arrange.
            Assert.IsType(typeReturned, calculatorStrategy);
        }
    }
}
