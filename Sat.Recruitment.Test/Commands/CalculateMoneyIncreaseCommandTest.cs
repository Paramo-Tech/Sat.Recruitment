using Moq;
using Sat.Recruitment.DTOs.Enums;
using Sat.Recruitment.Services.Commands.Imp;
using Sat.Recruitment.Services.Domain.MoneyIncrease;
using Xunit;

namespace Sat.Recruitment.Test.Commands
{
    public class CalculateMoneyIncreaseCommandTest
    {
        private readonly Mock<ICalculateMoneyIncreaseFactory> mockFactory;
        private readonly CalculateMoneyIncreaseCommand command;

        public CalculateMoneyIncreaseCommandTest()
        {
            mockFactory = new Mock<ICalculateMoneyIncreaseFactory>();
            command = new CalculateMoneyIncreaseCommand(mockFactory.Object);
        }

        [Theory]
        [InlineData(UserType.Normal, 150, 168)]
        [InlineData(UserType.Normal, 50, 90)]
        [InlineData(UserType.SuperUser, 150, 180)]
        [InlineData(UserType.Premium, 150, 450)]
        public void Execute_ShouldReturnExpectedResult(UserType userType, double money, double expectedMoneyIncrease)
        {
            var mockMoneyIncrease = new Mock<ICalculateMoneyIncrease>();
            mockMoneyIncrease.Setup(x => x.CalculateMoneyIncrease(money)).Returns(expectedMoneyIncrease);
            mockFactory.Setup(x => x.GetInstance(userType)).Returns(mockMoneyIncrease.Object);

            var result = command.Execute(userType, money);

            Assert.Equal(expectedMoneyIncrease, result);
        }
    }
}