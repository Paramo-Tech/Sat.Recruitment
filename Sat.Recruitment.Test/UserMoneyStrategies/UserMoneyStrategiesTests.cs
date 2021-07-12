using Domain.Factories;
using Domain.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.UserMoneyStrategies
{
    public class UserMoneyStrategiesTests
    {
        IUserMoneyStrategyFactory _strategyFactory;

        public UserMoneyStrategiesTests()
        {
            _strategyFactory = new UserMoneyStrategyFactory();
        }


        [Fact]
        public void SuperUser_Money_Should_Be_20_Percent_Up()
        {
            decimal money = 100;
            IUserMoneyStrategy superUserstrategy = _strategyFactory.BuildUserMoneyStrategy(Domain.Enums.UserTypes.SuperUser);
            decimal result = superUserstrategy.CalculateUserMoneyAmount(money);
            Assert.Equal(120, result);
        }

        [Fact]
        public void PremiumUser_Money_Should_Be_Multiplied_By_3_If_More_Than_100()
        {
            decimal money = 101;
            IUserMoneyStrategy superUserstrategy = _strategyFactory.BuildUserMoneyStrategy(Domain.Enums.UserTypes.Premium);
            decimal result = superUserstrategy.CalculateUserMoneyAmount(money);
            Assert.Equal(303, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(100)]
        public void PremiumUser_Money_Should_Be_Equal_To_Initial_Value_If_Equals_To_Or_Is_Less_Than_100(decimal money)
        {
            IUserMoneyStrategy superUserstrategy = _strategyFactory.BuildUserMoneyStrategy(Domain.Enums.UserTypes.Premium);
            decimal result = superUserstrategy.CalculateUserMoneyAmount(money);
            Assert.Equal(money, result);
        }


        [Fact]
        public void NormalUser_Money_Should_Be_12_Percent_Up_If_Initial_Amount_Is_More_Than_100()
        {
            decimal money = 101;
            IUserMoneyStrategy superUserstrategy = _strategyFactory.BuildUserMoneyStrategy(Domain.Enums.UserTypes.Normal);
            decimal result = superUserstrategy.CalculateUserMoneyAmount(money);
            Assert.Equal(Convert.ToDecimal(113.12), result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(50)]
        [InlineData(11)]
        public void NormalUser_Money_Should_Be_8_Percent_Up_If_Initial_Amount_Is_Equal_To_Or_Less_Than_100_And_Is_Higher_Than_10(decimal money)
        {
            decimal expected = money + (money * Convert.ToDecimal(0.8));
            IUserMoneyStrategy superUserstrategy = _strategyFactory.BuildUserMoneyStrategy(Domain.Enums.UserTypes.Normal);
            decimal result = superUserstrategy.CalculateUserMoneyAmount(money);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void NormalUser_Money_Should_Be_Equal_To_Initial_Value_If_Equals_To_Or_Is_Less_Than_10(decimal money)
        {
            IUserMoneyStrategy superUserstrategy = _strategyFactory.BuildUserMoneyStrategy(Domain.Enums.UserTypes.Normal);
            decimal result = superUserstrategy.CalculateUserMoneyAmount(money);
            Assert.Equal(money, result);
        }

    }
}
