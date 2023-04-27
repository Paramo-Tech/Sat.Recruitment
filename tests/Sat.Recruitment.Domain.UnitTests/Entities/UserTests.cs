using NUnit.Framework;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Shouldly;

namespace Sat.Recruitment.Domain.UnitTests.Entities
{
    public class Tests
    {
        [Test]
        // Arrange
        [TestCase(UserTypes.None, 0, 0)]
        [TestCase(UserTypes.None, 5, 5)]
        [TestCase(UserTypes.None, 10, 10)]
        [TestCase(UserTypes.None, 50, 50)]
        [TestCase(UserTypes.None, 100, 100)]
        [TestCase(UserTypes.None, 1000, 1000)]
        [TestCase(UserTypes.Normal, 0, 0)]
        [TestCase(UserTypes.Normal, 5, 5)]
        [TestCase(UserTypes.Normal, 10, 10)]
        [TestCase(UserTypes.Normal, 50, 90)]
        [TestCase(UserTypes.Normal, 100, 100)]
        [TestCase(UserTypes.Normal, 1000, 1120)]
        [TestCase(UserTypes.SuperUser, 0, 0)]
        [TestCase(UserTypes.SuperUser, 5, 5)]
        [TestCase(UserTypes.SuperUser, 10, 10)]
        [TestCase(UserTypes.SuperUser, 50, 50)]
        [TestCase(UserTypes.SuperUser, 100, 100)]
        [TestCase(UserTypes.SuperUser, 1000, 1200)]
        [TestCase(UserTypes.Premium, 0, 0)]
        [TestCase(UserTypes.Premium, 5, 5)]
        [TestCase(UserTypes.Premium, 10, 10)]
        [TestCase(UserTypes.Premium, 50, 50)]
        [TestCase(UserTypes.Premium, 100, 100)]
        [TestCase(UserTypes.Premium, 1000, 3000)]

        public void MoneyLogic(UserTypes userType, decimal money, decimal expected)
        {
            // Act
            var user = new User("Name", "Email", "Address", "Phone", userType, money, true);

            // Assert
            user.Money.ShouldBe(expected);
        }
    }
}
