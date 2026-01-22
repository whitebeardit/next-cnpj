using Xunit;
using next_CNPJ.Core.Utilities;

namespace next_CNPJ.Tests
{
    /// <summary>
    /// Tests for CNPJ formatting with masks.
    /// </summary>
    public class CnpjFormatterTests
    {
        [Fact]
        public void FormatWithMask_ValidNumericCnpj_ReturnsFormatted()
        {
            // Arrange
            var normalizedCnpj = "11222333000181";

            // Act
            var result = CnpjFormatter.FormatWithMask(normalizedCnpj);

            // Assert
            Assert.Equal("11.222.333/0001-81", result);
        }

        [Fact]
        public void FormatWithMask_ValidAlphanumericCnpj_ReturnsFormatted()
        {
            // Arrange
            var normalizedCnpj = "O14U9UHHBNQ434";

            // Act
            var result = CnpjFormatter.FormatWithMask(normalizedCnpj);

            // Assert
            Assert.Equal("O1.4U9.UHH/BNQ4-34", result);
        }

        [Fact]
        public void FormatWithMask_InvalidLength_ReturnsOriginal()
        {
            // Arrange
            var invalidCnpj = "123";

            // Act
            var result = CnpjFormatter.FormatWithMask(invalidCnpj);

            // Assert
            Assert.Equal("123", result);
        }

        [Fact]
        public void FormatWithMask_NullOrEmpty_ReturnsOriginal()
        {
            // Arrange
            string nullCnpj = null!;
            string emptyCnpj = "";

            // Act
            var resultNull = CnpjFormatter.FormatWithMask(nullCnpj);
            var resultEmpty = CnpjFormatter.FormatWithMask(emptyCnpj);

            // Assert
            Assert.Equal("", resultNull);
            Assert.Equal("", resultEmpty);
        }
    }
}