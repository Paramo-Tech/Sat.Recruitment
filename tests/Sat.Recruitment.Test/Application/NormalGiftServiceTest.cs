using Application.Services;
using System;
using Xunit;

namespace Sat.Recruitment.Test.Application
{
    public class NormalGiftServiceTest
    {
        private readonly NormalGiftService _service;

        public NormalGiftServiceTest()
        {
            _service = new NormalGiftService();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(103)]
        [InlineData(1150)]
        [InlineData(158)]
        [InlineData(155)]
        [InlineData(2852)]
        public void GivenMoreThan100WhenHandleNormalUserThenReturnMoneyWith012Percentage(decimal money)
        {
            //Arrange            
            var percentage = Convert.ToDecimal(0.12);
            var gif = money * percentage;
            var expected = money + gif;

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(100)]
        [InlineData(99)]
        [InlineData(98)]
        [InlineData(70)]
        [InlineData(60)]
        [InlineData(50)]
        public void GivenLessThanOrEqualTo100WhenHandleNormalUserThenReturnMoneyWith08Percentage(decimal money)
        {
            //Arrange                        
            var percentage = Convert.ToDecimal(0.8);
            var gif = money * percentage;
            var expected = money + gif;

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(9)]
        [InlineData(8)]
        [InlineData(7)]
        [InlineData(6)]
        [InlineData(3)]
        public void GivenLessThanOrEqualTo10WhenHandleNormalUserThenReturnTheSameMoney(decimal money)
        {
            //Arrange                        

            //Act
            var result = _service.GetDiscount(money);

            //Assert
            Assert.Equal(money, result);
        }
    }

}