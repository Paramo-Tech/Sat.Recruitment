using Application.Services;
using System;
using Xunit;

namespace Sat.Recruitment.Test.Application
{
    public class PremiumGiftServiceTest
    {
        private readonly PremiumGiftService _service;

        public PremiumGiftServiceTest()
        {
            _service = new PremiumGiftService();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(103)]
        [InlineData(1150)]
        [InlineData(158)]
        [InlineData(155)]
        [InlineData(2852)]
        public void GivenMoreThan100WhenHandlePremiumUserThenReturnMoneyWith2Percentage(decimal money)
        {
            //Arrange            
            var percentage = Convert.ToDecimal(2);
            var gif = money * percentage;
            var expected = money + gif;

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(65)]
        [InlineData(85)]
        [InlineData(15)]
        [InlineData(13)]
        [InlineData(2)]
        public void GivenLessThanOrEqualTo100WhenHandlePremiumUserThenReturnTheSameMoney(decimal money)
        {
            //Arrange                        

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(money, result);
        }
    }
}
