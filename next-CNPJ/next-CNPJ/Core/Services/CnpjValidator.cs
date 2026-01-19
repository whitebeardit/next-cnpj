using System.Linq;
using next_CNPJ.Core.Domain;
using next_CNPJ.Core.Utilities;

namespace next_CNPJ.Core.Services
{
    /// <summary>
    /// Implementação do validador de CNPJ conforme NTC 2025.001.
    /// Suporta formato numérico tradicional e novo formato alfanumérico.
    /// </summary>
    public class CnpjValidator : ICnpjValidator
    {
        private const int ExpectedLength = 14;
        private const int RootStartIndex = 0;
        private const int RootEndIndex = 8;
        private const int OrderStartIndex = 8;
        private const int OrderEndIndex = 12;
        private const int DigitStartIndex = 12;

        private readonly ICnpjFormatIdentifier _formatIdentifier;

        /// <summary>
        /// Cria uma instância do validador.
        /// </summary>
        public CnpjValidator() : this(new CnpjFormatIdentifier())
        {
        }

        /// <summary>
        /// Cria uma instância do validador com um identificador de formato customizado.
        /// </summary>
        /// <param name="formatIdentifier">Identificador de formato a ser usado.</param>
        public CnpjValidator(ICnpjFormatIdentifier formatIdentifier)
        {
            _formatIdentifier = formatIdentifier;
        }

        /// <summary>
        /// Valida um CNPJ conforme as regras da NTC 2025.001.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado (pode conter formatação).</param>
        /// <param name="config">Configuração opcional para validação. Se null, usa configuração padrão.</param>
        /// <returns>Resultado da validação com informações detalhadas.</returns>
        public CnpjValidationResult Validate(string? cnpj, CnpjConfiguration? config = null)
        {
            config ??= new CnpjConfiguration();

            // Normaliza o CNPJ
            var normalized = CnpjNormalizer.Normalize(cnpj);

            // Valida tamanho
            if (normalized.Length != ExpectedLength)
            {
                return new CnpjValidationResult(
                    $"CNPJ deve ter exatamente {ExpectedLength} caracteres. Fornecido: {normalized.Length}.",
                    normalized
                );
            }

            // Identifica formato
            var format = _formatIdentifier.IdentifyFormat(normalized);

            // Valida estrutura por partes
            var root = normalized.Substring(RootStartIndex, RootEndIndex - RootStartIndex);
            var order = normalized.Substring(OrderStartIndex, OrderEndIndex - OrderStartIndex);
            var digits = normalized.Substring(DigitStartIndex);

            // Valida dígitos verificadores (devem ser numéricos)
            if (!digits.All(char.IsDigit))
            {
                return new CnpjValidationResult(
                    "Os dígitos verificadores (posições 13 e 14) devem ser exclusivamente numéricos.",
                    normalized,
                    format
                );
            }

            // Valida raiz (posições 1-8)
            var rootValidation = ValidateSegment(root, "raiz", config);
            if (!rootValidation.IsValid)
            {
                return new CnpjValidationResult(rootValidation.ErrorMessage!, normalized, format);
            }

            // Valida ordem (posições 9-12)
            var orderValidation = ValidateSegment(order, "ordem", config);
            if (!orderValidation.IsValid)
            {
                return new CnpjValidationResult(orderValidation.ErrorMessage!, normalized, format);
            }

            // Valida dígitos verificadores
            if (!DigitVerifierCalculator.ValidateDigits(normalized))
            {
                return new CnpjValidationResult(
                    "Dígitos verificadores inválidos.",
                    normalized,
                    format
                );
            }

            return new CnpjValidationResult(format, normalized);
        }

        /// <summary>
        /// Verifica se um CNPJ é válido.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado (pode conter formatação).</param>
        /// <param name="config">Configuração opcional para validação. Se null, usa configuração padrão.</param>
        /// <returns>True se o CNPJ é válido.</returns>
        public bool IsValid(string? cnpj, CnpjConfiguration? config = null)
        {
            return Validate(cnpj, config).IsValid;
        }

        /// <summary>
        /// Valida um segmento do CNPJ (raiz ou ordem).
        /// </summary>
        /// <param name="segment">Segmento a validar.</param>
        /// <param name="segmentName">Nome do segmento (para mensagens de erro).</param>
        /// <param name="config">Configuração de validação.</param>
        /// <returns>Resultado da validação do segmento.</returns>
        private CnpjValidationResult ValidateSegment(string segment, string segmentName, CnpjConfiguration config)
        {
            foreach (var character in segment)
            {
                // Verifica se é alfanumérico
                if (!char.IsLetterOrDigit(character))
                {
                    return new CnpjValidationResult(
                        $"O segmento {segmentName} contém caracteres inválidos. Apenas letras (A-Z) e números (0-9) são permitidos.",
                        string.Empty
                    );
                }

                // Se for letra, verifica se está na lista de excluídas
                if (char.IsLetter(character) && config.IsLetterExcluded(character))
                {
                    return new CnpjValidationResult(
                        $"O segmento {segmentName} contém a letra '{character}' que não é permitida. Letras excluídas: {string.Join(", ", config.ExcludedLetters)}.",
                        string.Empty
                    );
                }
            }

            return new CnpjValidationResult(CnpjFormat.Numeric, segment);
        }
    }
}
