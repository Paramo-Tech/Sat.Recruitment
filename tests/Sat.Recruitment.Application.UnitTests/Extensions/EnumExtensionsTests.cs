using NUnit.Framework;
using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Domain.Enums;
using Shouldly;

namespace Sat.Recruitment.Application.UnitTests.Extensions
{
    public class EnumExtensionsTests
    {
        [Test]
        // Arrange
        [TestCase(UserTypes.None, nameof(UserTypes.None))]
        [TestCase(UserTypes.Normal, nameof(UserTypes.Normal))]
        [TestCase(UserTypes.SuperUser, nameof(UserTypes.SuperUser))]
        [TestCase(UserTypes.Premium, nameof(UserTypes.Premium))]
        public void EnumExtensionsTest(UserTypes userType, string expected)
        {
            // Act
            string displayName = userType.DisplayName();

            // Assert
            displayName.ShouldBe(expected);
        }

        [Test]
        public void EnumExtensionsTest()
        {
            // Arrange / Act
            string displayName = TestEnum.NoDescription.DisplayName();

            // Assert
            displayName.ShouldBe(nameof(TestEnum.NoDescription));
        }

        private enum TestEnum
        {
            NoDescription,
            Description
        }
    }
}
