using Moq;
using Sat.Recruitment.Application.Services;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class MoneyCalculartorTest
    {
        [Theory()]
        [InlineData(110, "normal", 123.2)]
        [InlineData(100, "normal", 180)]
        [InlineData(200, "superuser", 240)]
        [InlineData(1000, "premium", 3000)]
        [InlineData(1000, "other", 1000)]
        public void CalculateTest_MustMatch(decimal money, string userType, decimal moneyExpected)
        {
            MoneyCalculatorService service = new MoneyCalculatorService();
            var user = Mock.Of<User>();
            user.Money = money;
            user.UserType = userType;

            service.CalculateMoney(user);

            Assert.Equal(user.Money, moneyExpected);
        }
    }
}
