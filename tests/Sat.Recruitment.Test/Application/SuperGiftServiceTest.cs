using Application.Services;
using System;
using Xunit;

namespace Sat.Recruitment.Test.Application
{
    public class SuperGiftServiceTest
    {
        private readonly SuperGiftService _service;

        public SuperGiftServiceTest()
        {
            _service = new SuperGiftService();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(103)]
        [InlineData(1150)]
        [InlineData(158)]
        [InlineData(155)]
        [InlineData(2852)]
        public void GivenMoreThan100WhenHandleSuperUserThenReturnMoneyWith020Percentage(decimal money)
        {
            //Arrange            
            var percentage = Convert.ToDecimal(0.20);
            var gif = money * percentage;
            var expected = money + gif;

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(98)]
        [InlineData(50)]
        [InlineData(70)]
        [InlineData(30)]
        [InlineData(20)]
        public void GivenLessThanOrEqualTo100WhenHandleSuperlUserThenReturnTheSameMoney(decimal money)
        {
            //Arrange                        

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(money, result);
        }
    }
}
