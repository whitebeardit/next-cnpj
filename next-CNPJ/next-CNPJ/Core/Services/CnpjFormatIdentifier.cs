using System.Linq;
using next_CNPJ.Core.Domain;
using next_CNPJ.Core.Utilities;

namespace next_CNPJ.Core.Services
{
    /// <summary>
    /// Implementação para identificação do formato de CNPJ.
    /// </summary>
    public class CnpjFormatIdentifier : ICnpjFormatIdentifier
    {
        private const int ExpectedLength = 14;
        private const int RootEndIndex = 8;
        private const int OrderEndIndex = 12;

        /// <summary>
        /// Identifica o formato do CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser analisado (pode conter formatação).</param>
        /// <returns>Formato identificado (Numeric ou Alphanumeric).</returns>
        public CnpjFormat IdentifyFormat(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return CnpjFormat.Numeric;

            var normalized = CnpjNormalizer.Normalize(cnpj);

            if (normalized.Length != ExpectedLength)
                return CnpjFormat.Numeric;

            // Verifica se há letras na raiz (posições 1-8) ou ordem (posições 9-12)
            var root = normalized.Substring(0, RootEndIndex);
            var order = normalized.Substring(RootEndIndex, OrderEndIndex - RootEndIndex);

            var hasLettersInRoot = root.Any(char.IsLetter);
            var hasLettersInOrder = order.Any(char.IsLetter);

            return (hasLettersInRoot || hasLettersInOrder) 
                ? CnpjFormat.Alphanumeric 
                : CnpjFormat.Numeric;
        }

        /// <summary>
        /// Verifica se o CNPJ é alfanumérico.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser analisado (pode conter formatação).</param>
        /// <returns>True se o CNPJ contém letras na raiz ou ordem.</returns>
        public bool IsAlphanumeric(string? cnpj)
        {
            return IdentifyFormat(cnpj) == CnpjFormat.Alphanumeric;
        }

        /// <summary>
        /// Verifica se o CNPJ é exclusivamente numérico.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser analisado (pode conter formatação).</param>
        /// <returns>True se o CNPJ contém apenas números.</returns>
        public bool IsNumeric(string? cnpj)
        {
            return IdentifyFormat(cnpj) == CnpjFormat.Numeric;
        }
    }
}
