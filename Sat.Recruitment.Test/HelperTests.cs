using Sat.Recruitment.BLL.utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class HelperTests
    {
        [Theory]
        [InlineData(nameof(UserType.NORMAL), 200, 224)]
        [InlineData(nameof(UserType.NORMAL), 50, 90)]
        [InlineData(nameof(UserType.SUPERUSER), 200, 240)]
        [InlineData(nameof(UserType.PREMIUM), 200, 600)]
        public void CalculateGif_ValidInputs_ReturnsExpectedResult(string userType, decimal money, decimal expected)
        {
            // Act
            var result = Helper.CalculateGif(userType, money);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestValidateErrors()
        {
            // Arrange
            string name = null;
            string email = null;
            string address = null;
            string phone = null;

            // Act
            List<string> errors = Helper.ValidateErrors(name, email, address, phone);

            // Assert
            Assert.Equal(4, errors.Count);
            Assert.Contains("The name is required", errors);
            Assert.Contains("The email is required", errors);
            Assert.Contains("The address is required", errors);
            Assert.Contains("The phone is required", errors);
        }

        [Fact]
        public void NormalizeEmail_ThrowsException_WhenEmailIsNull()
        {
            // Arrange
            string email = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Helper.NormalizeEmail(email));
        }

        [Fact]
        public void NormalizeEmail_ThrowsException_WhenEmailIsEmpty()
        {
            // Arrange
            string email = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Helper.NormalizeEmail(email));
        }

        [Fact]
        public void NormalizeEmail_ThrowsException_WhenEmailIsWhitespace()
        {
            // Arrange
            string email = "  \t  ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Helper.NormalizeEmail(email));
        }

        [Fact]
        public void NormalizeEmail_ThrowsException_WhenEmailHasNoAtSymbol()
        {
            // Arrange
            string email = "johndoe.example.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Helper.NormalizeEmail(email));
        }

        [Fact]
        public void NormalizeEmail_NormalizesEmail_WhenEmailHasPeriodsAndPlusSymbol()
        {
            // Arrange
            string email = "johndoe.example+test@gmail.com";

            // Act
            string normalizedEmail = Helper.NormalizeEmail(email);

            // Assert
            Assert.Equal("johndoeexampletest@gmail.com", normalizedEmail);
        }

        [Fact]
        public void NormalizeEmail_NormalizesEmail_WhenEmailHasPeriodsButNoPlusSymbol()
        {
            // Arrange
            string email = "johndoe.example@gmail.com";

            // Act
            string normalizedEmail = Helper.NormalizeEmail(email);

            // Assert
            Assert.Equal("johndoeexample@gmail.com", normalizedEmail);
        }

        [Fact]
        public void NormalizeEmail_ReturnsOriginalEmail_WhenEmailHasPlusSymbolButNoPeriods()
        {
            // Arrange
            string email = "johndoe+test@gmail.com";

            // Act
            string normalizedEmail = Helper.NormalizeEmail(email);

            // Assert
            Assert.Equal("johndoetest@gmail.com", normalizedEmail);
        }

        [Fact]
        public void NormalizeEmail_ReturnsOriginalEmail_WhenEmailHasNoPeriodsOrPlusSymbol()
        {
            // Arrange
            string email = "johndoe@gmail.com";

            // Act
            string normalizedEmail = Helper.NormalizeEmail(email);

            // Assert
            Assert.Equal("johndoe@gmail.com", normalizedEmail);
        }

    }

}
