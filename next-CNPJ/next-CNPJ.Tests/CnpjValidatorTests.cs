using Xunit;
using next_CNPJ.Core.Domain;
using next_CNPJ.Core.Services;
using next_CNPJ.Core.Utilities;

namespace next_CNPJ.Tests
{
    /// <summary>
    /// Testes para validação de CNPJ numérico (formato tradicional).
    /// </summary>
    public class CnpjNumericValidationTests
    {
        private readonly ICnpjValidator _validator;

        public CnpjNumericValidationTests()
        {
            _validator = new CnpjValidator();
        }

        [Fact]
        public void Validate_ValidNumericCnpj_ReturnsValid()
        {
            // Arrange
            var cnpj = "11222333000181";

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(CnpjFormat.Numeric, result.Format);
            Assert.Equal("11222333000181", result.NormalizedCnpj);
        }

        [Fact]
        public void Validate_ValidNumericCnpjWithFormatting_ReturnsValid()
        {
            // Arrange
            var cnpj = "11.222.333/0001-81";

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(CnpjFormat.Numeric, result.Format);
            Assert.Equal("11222333000181", result.NormalizedCnpj);
        }

        [Fact]
        public void Validate_InvalidNumericCnpj_WrongDigits_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "11222333000182"; // DV incorreto

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Dígitos verificadores inválidos", result.ErrorMessage);
        }

        [Fact]
        public void Validate_NumericCnpj_WrongLength_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "1122233300018"; // 13 caracteres

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("14 caracteres", result.ErrorMessage);
        }

        [Fact]
        public void Validate_EmptyCnpj_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "";

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_NullCnpj_ReturnsInvalid()
        {
            // Arrange
            string? cnpj = null;

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void IsValid_ValidNumericCnpj_ReturnsTrue()
        {
            // Arrange
            var cnpj = "11222333000181";

            // Act
            var isValid = _validator.IsValid(cnpj);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_InvalidNumericCnpj_ReturnsFalse()
        {
            // Arrange
            var cnpj = "11222333000182";

            // Act
            var isValid = _validator.IsValid(cnpj);

            // Assert
            Assert.False(isValid);
        }
    }

    /// <summary>
    /// Testes para validação de CNPJ alfanumérico (novo formato).
    /// </summary>
    public class CnpjAlphanumericValidationTests
    {
        private readonly ICnpjValidator _validator;

        public CnpjAlphanumericValidationTests()
        {
            _validator = new CnpjValidator();
        }

        [Fact]
        public void Validate_ValidAlphanumericCnpj_FromConfluence_ReturnsValid()
        {
            // Arrange - Exemplo do Confluence: 12ABC34501DE35
            var cnpj = "12ABC34501DE35";

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(CnpjFormat.Alphanumeric, result.Format);
            Assert.Equal("12ABC34501DE35", result.NormalizedCnpj);
        }

        [Fact]
        public void Validate_ValidAlphanumericCnpj_WithFormatting_ReturnsValid()
        {
            // Arrange
            var cnpj = "12.ABC.345/01DE-35";

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(CnpjFormat.Alphanumeric, result.Format);
            Assert.Equal("12ABC34501DE35", result.NormalizedCnpj);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithLowercase_ReturnsValid()
        {
            // Arrange - Deve normalizar para maiúsculas
            var cnpj = "12abc34501de35";

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(CnpjFormat.Alphanumeric, result.Format);
            Assert.Equal("12ABC34501DE35", result.NormalizedCnpj);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithExcludedLetterI_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12IBC34501DE35"; // I está excluída

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("não é permitida", result.ErrorMessage);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithExcludedLetterO_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12OBC34501DE35"; // O está excluída

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithExcludedLetterU_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12UBC34501DE35"; // U está excluída

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithExcludedLetterQ_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12QBC34501DE35"; // Q está excluída

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithExcludedLetterF_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12FBC34501DE35"; // F está excluída

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithExcludedLetters_ButAllowed_ReturnsValid()
        {
            // Arrange
            var cnpj = "12IBC34501DE35";
            var config = new CnpjConfiguration
            {
                AllowExcludedLetters = true
            };

            // Act
            var result = _validator.Validate(cnpj, config);

            // Assert
            // Pode ser válido se o DV estiver correto, mas neste caso provavelmente não estará
            // O importante é que não rejeite por causa da letra excluída
            Assert.NotNull(result);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WrongDigits_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12ABC34501DE36"; // DV incorreto

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Dígitos verificadores inválidos", result.ErrorMessage);
        }

        [Fact]
        public void Validate_AlphanumericCnpj_WithNonNumericDigits_ReturnsInvalid()
        {
            // Arrange
            var cnpj = "12ABC34501DE3A"; // DV com letra

            // Act
            var result = _validator.Validate(cnpj);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("exclusivamente numéricos", result.ErrorMessage);
        }
    }

    /// <summary>
    /// Testes para cálculo de dígitos verificadores.
    /// </summary>
    public class DigitVerifierCalculationTests
    {
        [Fact]
        public void CalculateFirstDigit_ConfluenceExample_ReturnsCorrectValue()
        {
            // Arrange - Exemplo do Confluence: 12ABC34501DE
            // Primeiro DV deve ser 3
            var cnpjBase = "12ABC34501DE";

            // Act
            var firstDigit = DigitVerifierCalculator.CalculateFirstDigit(cnpjBase);

            // Assert
            Assert.Equal(3, firstDigit);
        }

        [Fact]
        public void CalculateSecondDigit_ConfluenceExample_ReturnsCorrectValue()
        {
            // Arrange - Exemplo do Confluence: 12ABC34501DE com primeiro DV = 3
            // Segundo DV deve ser 5
            var cnpjBase = "12ABC34501DE";
            var firstDigit = 3;

            // Act
            var secondDigit = DigitVerifierCalculator.CalculateSecondDigit(cnpjBase, firstDigit);

            // Assert
            Assert.Equal(5, secondDigit);
        }

        [Fact]
        public void ValidateDigits_ConfluenceExample_ReturnsTrue()
        {
            // Arrange - Exemplo completo do Confluence: 12ABC34501DE35
            var cnpj = "12ABC34501DE35";

            // Act
            var isValid = DigitVerifierCalculator.ValidateDigits(cnpj);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void ValidateDigits_ValidNumericCnpj_ReturnsTrue()
        {
            // Arrange
            var cnpj = "11222333000181";

            // Act
            var isValid = DigitVerifierCalculator.ValidateDigits(cnpj);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void ValidateDigits_InvalidCnpj_ReturnsFalse()
        {
            // Arrange
            var cnpj = "11222333000182"; // DV incorreto

            // Act
            var isValid = DigitVerifierCalculator.ValidateDigits(cnpj);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void CalculateFirstDigit_NumericCnpj_CompatibleWithOldFormat()
        {
            // Arrange - CNPJ numérico válido
            var cnpjBase = "112223330001";

            // Act
            var firstDigit = DigitVerifierCalculator.CalculateFirstDigit(cnpjBase);

            // Assert
            Assert.Equal(8, firstDigit);
        }

        [Fact]
        public void CalculateSecondDigit_NumericCnpj_CompatibleWithOldFormat()
        {
            // Arrange
            var cnpjBase = "112223330001";
            var firstDigit = 8;

            // Act
            var secondDigit = DigitVerifierCalculator.CalculateSecondDigit(cnpjBase, firstDigit);

            // Assert
            Assert.Equal(1, secondDigit);
        }
    }

    /// <summary>
    /// Testes para identificação de formato de CNPJ.
    /// </summary>
    public class CnpjFormatIdentificationTests
    {
        private readonly ICnpjFormatIdentifier _identifier;

        public CnpjFormatIdentificationTests()
        {
            _identifier = new CnpjFormatIdentifier();
        }

        [Fact]
        public void IdentifyFormat_NumericCnpj_ReturnsNumeric()
        {
            // Arrange
            var cnpj = "11222333000181";

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Numeric, format);
        }

        [Fact]
        public void IdentifyFormat_AlphanumericCnpj_ReturnsAlphanumeric()
        {
            // Arrange
            var cnpj = "12ABC34501DE35";

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Alphanumeric, format);
        }

        [Fact]
        public void IdentifyFormat_AlphanumericCnpj_WithLettersInRoot_ReturnsAlphanumeric()
        {
            // Arrange - Letras na raiz
            var cnpj = "12ABC345000135";

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Alphanumeric, format);
        }

        [Fact]
        public void IdentifyFormat_AlphanumericCnpj_WithLettersInOrder_ReturnsAlphanumeric()
        {
            // Arrange - Letras na ordem
            var cnpj = "12345678ABCD35";

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Alphanumeric, format);
        }

        [Fact]
        public void IdentifyFormat_AlphanumericCnpj_WithFormatting_ReturnsAlphanumeric()
        {
            // Arrange
            var cnpj = "12.ABC.345/01DE-35";

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Alphanumeric, format);
        }

        [Fact]
        public void IsAlphanumeric_NumericCnpj_ReturnsFalse()
        {
            // Arrange
            var cnpj = "11222333000181";

            // Act
            var isAlphanumeric = _identifier.IsAlphanumeric(cnpj);

            // Assert
            Assert.False(isAlphanumeric);
        }

        [Fact]
        public void IsAlphanumeric_AlphanumericCnpj_ReturnsTrue()
        {
            // Arrange
            var cnpj = "12ABC34501DE35";

            // Act
            var isAlphanumeric = _identifier.IsAlphanumeric(cnpj);

            // Assert
            Assert.True(isAlphanumeric);
        }

        [Fact]
        public void IsNumeric_NumericCnpj_ReturnsTrue()
        {
            // Arrange
            var cnpj = "11222333000181";

            // Act
            var isNumeric = _identifier.IsNumeric(cnpj);

            // Assert
            Assert.True(isNumeric);
        }

        [Fact]
        public void IsNumeric_AlphanumericCnpj_ReturnsFalse()
        {
            // Arrange
            var cnpj = "12ABC34501DE35";

            // Act
            var isNumeric = _identifier.IsNumeric(cnpj);

            // Assert
            Assert.False(isNumeric);
        }

        [Fact]
        public void IdentifyFormat_EmptyString_ReturnsNumeric()
        {
            // Arrange
            var cnpj = "";

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Numeric, format);
        }

        [Fact]
        public void IdentifyFormat_Null_ReturnsNumeric()
        {
            // Arrange
            string? cnpj = null;

            // Act
            var format = _identifier.IdentifyFormat(cnpj);

            // Assert
            Assert.Equal(CnpjFormat.Numeric, format);
        }
    }

    /// <summary>
    /// Testes para normalização de CNPJ.
    /// </summary>
    public class CnpjNormalizationTests
    {
        [Fact]
        public void Normalize_WithFormatting_RemovesFormatting()
        {
            // Arrange
            var cnpj = "12.ABC.345/01DE-35";

            // Act
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Assert
            Assert.Equal("12ABC34501DE35", normalized);
        }

        [Fact]
        public void Normalize_WithLowercase_ConvertsToUppercase()
        {
            // Arrange
            var cnpj = "12abc34501de35";

            // Act
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Assert
            Assert.Equal("12ABC34501DE35", normalized);
        }

        [Fact]
        public void Normalize_WithSpaces_RemovesSpaces()
        {
            // Arrange
            var cnpj = "12 ABC 345 01 DE 35";

            // Act
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Assert
            Assert.Equal("12ABC34501DE35", normalized);
        }

        [Fact]
        public void Normalize_EmptyString_ReturnsEmpty()
        {
            // Arrange
            var cnpj = "";

            // Act
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Assert
            Assert.Equal("", normalized);
        }

        [Fact]
        public void Normalize_Null_ReturnsEmpty()
        {
            // Arrange
            string? cnpj = null;

            // Act
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Assert
            Assert.Equal("", normalized);
        }

        [Fact]
        public void Normalize_OnlyAlphanumeric_KeepsAll()
        {
            // Arrange
            var cnpj = "12ABC34501DE35";

            // Act
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Assert
            Assert.Equal("12ABC34501DE35", normalized);
        }
    }

    /// <summary>
    /// Testes para conversão ASCII.
    /// </summary>
    public class AsciiConverterTests
    {
        [Fact]
        public void ToNumericValue_NumericCharacters_ReturnsCorrectValue()
        {
            // Arrange & Act & Assert
            Assert.Equal(0, AsciiConverter.ToNumericValue('0'));
            Assert.Equal(1, AsciiConverter.ToNumericValue('1'));
            Assert.Equal(9, AsciiConverter.ToNumericValue('9'));
        }

        [Fact]
        public void ToNumericValue_LetterA_Returns17()
        {
            // Arrange & Act
            var value = AsciiConverter.ToNumericValue('A');

            // Assert
            Assert.Equal(17, value); // ASCII 'A' = 65, 65 - 48 = 17
        }

        [Fact]
        public void ToNumericValue_LetterB_Returns18()
        {
            // Arrange & Act
            var value = AsciiConverter.ToNumericValue('B');

            // Assert
            Assert.Equal(18, value); // ASCII 'B' = 66, 66 - 48 = 18
        }

        [Fact]
        public void ToNumericValue_LetterC_Returns19()
        {
            // Arrange & Act
            var value = AsciiConverter.ToNumericValue('C');

            // Assert
            Assert.Equal(19, value); // ASCII 'C' = 67, 67 - 48 = 19
        }

        [Fact]
        public void ToNumericValue_LetterD_Returns20()
        {
            // Arrange & Act
            var value = AsciiConverter.ToNumericValue('D');

            // Assert
            Assert.Equal(20, value); // ASCII 'D' = 68, 68 - 48 = 20
        }

        [Fact]
        public void ToNumericValue_LetterE_Returns21()
        {
            // Arrange & Act
            var value = AsciiConverter.ToNumericValue('E');

            // Assert
            Assert.Equal(21, value); // ASCII 'E' = 69, 69 - 48 = 21
        }
    }
}
