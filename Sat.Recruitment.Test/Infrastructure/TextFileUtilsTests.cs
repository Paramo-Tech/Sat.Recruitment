using Sat.Recruitment.Infrastructure.TextFile;
using System;
using Xunit;

namespace Sat.Recruitment.Test.Infrastructure
{
    public class TextFileUtilsTests
    {
        [Fact]
        public void GetElementFromPosition_WithValidParameters_ReturnsElement()
        {
            // Arrange.
            var expectedElement = "hello";
            var line = $"Well,{expectedElement},there.";

            // Act.
            var result = TextFileUtils.GetElementFromLine(1, line);

            // Assert.
            Assert.Equal(expectedElement, result);
        }

        [Fact]
        public void GetElementFromPosition_WithInvalidPosition_ThrowsException()
        {
            // Arrange.
            var expectedElement = "hello";
            var line = $"Well,{expectedElement},there.";

            // Act.
            void getElement () => TextFileUtils.GetElementFromLine(27, line);

            // Assert.
            Assert.Throws<InvalidOperationException>(getElement);
        }
    }
}
