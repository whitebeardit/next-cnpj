namespace next_CNPJ.Core.Utilities
{
    /// <summary>
    /// Utility class for formatting CNPJ strings with the standard Brazilian mask.
    /// </summary>
    public static class CnpjFormatter
    {
        private const int ExpectedLength = 14;

        /// <summary>
        /// Formats a CNPJ with the standard Brazilian mask: XX.XXX.XXX/XXXX-XX.
        /// </summary>
        /// <param name="cnpj">
        /// The CNPJ string. Can be normalized (14 chars) or contain formatting/separators; it will be normalized internally.
        /// </param>
        /// <returns>
        /// The formatted CNPJ string when the normalized value has 14 characters; otherwise returns the original input.
        /// Returns empty string when input is null or whitespace.
        /// </returns>
        public static string FormatWithMask(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return string.Empty;

            var normalized = CnpjNormalizer.Normalize(cnpj);
            if (normalized.Length != ExpectedLength)
                return cnpj!;

            return $"{normalized.Substring(0, 2)}.{normalized.Substring(2, 3)}.{normalized.Substring(5, 3)}/{normalized.Substring(8, 4)}-{normalized.Substring(12, 2)}";
        }

        /// <summary>
        /// Tries to format a CNPJ with the standard Brazilian mask: XX.XXX.XXX/XXXX-XX.
        /// </summary>
        /// <param name="cnpj">
        /// The CNPJ string. Can be normalized (14 chars) or contain formatting/separators; it will be normalized internally.
        /// </param>
        /// <param name="formatted">
        /// When successful, receives the formatted CNPJ. When not successful, receives empty string if input is null/whitespace,
        /// otherwise receives the original input.
        /// </param>
        /// <returns>True when formatting succeeded (normalized length is 14); otherwise false.</returns>
        public static bool TryFormatWithMask(string? cnpj, out string formatted)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                formatted = string.Empty;
                return false;
            }

            var normalized = CnpjNormalizer.Normalize(cnpj);
            if (normalized.Length != ExpectedLength)
            {
                formatted = cnpj!;
                return false;
            }

            formatted =
                $"{normalized.Substring(0, 2)}.{normalized.Substring(2, 3)}.{normalized.Substring(5, 3)}/{normalized.Substring(8, 4)}-{normalized.Substring(12, 2)}";
            return true;
        }
    }
}
