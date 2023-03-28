using FluentAssertions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Services.Services;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    public class CalculateUserGiftValueTests
    {
        private readonly IUserCalculateGiftValue _userCalculateGiftValue;

        public CalculateUserGiftValueTests()
        {
            _userCalculateGiftValue = new UserCalculateGiftValue();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(10.001, 8.0008)]
        [InlineData(20, 16)]
        [InlineData(100, 0)]
        [InlineData(200, 24)]
        public void Calculate_ShouldBeCorrect_WhenUserTypeIsNormal(decimal money, decimal result)
        {
            //Arrange
            var percentage = _userCalculateGiftValue.GetGiftValue("Normal", money);
            percentage.Should().Be(result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 0)]
        [InlineData(200, 40)]
        public void Calculate_ShouldBeCorrect_WhenUserTypeIsSuperUser(decimal money, decimal result)
        {
            //Arrange
            var percentage = _userCalculateGiftValue.GetGiftValue("SuperUser", money);
            percentage.Should().Be(result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 0)]
        [InlineData(200, 400)]
        public void Calculate_ShouldBeCorrect_WhenUserTypeIsPremium(decimal money, decimal result)
        {
            //Arrange
            var percentage = _userCalculateGiftValue.GetGiftValue("Premium", money);
            percentage.Should().Be(result);
        }
    }
}
