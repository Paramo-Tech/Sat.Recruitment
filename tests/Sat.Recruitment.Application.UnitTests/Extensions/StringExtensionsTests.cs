using System;
using NUnit.Framework;
using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Domain.Enums;
using Shouldly;

namespace Sat.Recruitment.Application.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Test]
        // Arrange
        [TestCase(null, UserTypes.None)]
        [TestCase("", UserTypes.None)]
        [TestCase("Invalid", UserTypes.None)]
        [TestCase(nameof(UserTypes.None), UserTypes.None)]
        [TestCase(nameof(UserTypes.Normal), UserTypes.Normal)]
        [TestCase(nameof(UserTypes.SuperUser), UserTypes.SuperUser)]
        [TestCase(nameof(UserTypes.Premium), UserTypes.Premium)]
        public void ToUserTypesTest(string input, UserTypes expected)
        {
            // Act
            UserTypes userType = input.ToUserTypes();

            // Assert
            userType.ShouldBe(expected);
        }

        [Test]
        // Arrange
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("email@email.com", "email@email.com")]
        [TestCase("em.ail@email.com", "email@email.com")]
        [TestCase("em+ail@email.com", "email@email.com")]
        [TestCase("  email@email.com", "email@email.com")]
        [TestCase("  em+a.il@email.com", "email@email.com")]
        public void NormalizeEmailTest(string input, string expected)
        {
            // Act
            string email = input.NormalizeEmail();

            // Assert
            email.ShouldBe(expected);
        }

        [Test]
        // Arrange
        [TestCase("ema@il@email.com")]
        [TestCase("email@ema@il.com")]
        [TestCase(".email@email.com")]
        [TestCase(".email@email.c")]
        [TestCase(".email@email..com")]
        [TestCase(".email@ema#il.com")]
        public void NormalizeEmailTest(string input)
        {
            // Act / Assert
            Assert.Throws<ArgumentException>(() => input.NormalizeEmail());
        }
    }
}
