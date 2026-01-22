using System;

namespace next_CNPJ.Core.Utilities
{
    /// <summary>
    /// Utility class for formatting CNPJ strings with standard masks.
    /// </summary>
    public static class CnpjFormatter
    {
        /// <summary>
        /// Formats a normalized CNPJ (14 characters) with the standard Brazilian mask: XX.XXX.XXX/XXXX-XX.
        /// </summary>
        /// <param name="normalizedCnpj">The normalized CNPJ string (14 characters, no formatting).</param>
        /// <returns>The formatted CNPJ string, or the original if invalid length.</returns>
        public static string FormatWithMask(string? normalizedCnpj)
        {
            if (normalizedCnpj is null || normalizedCnpj.Length != 14)
                return normalizedCnpj ?? string.Empty;

            return $"{normalizedCnpj.Substring(0, 2)}.{normalizedCnpj.Substring(2, 3)}.{normalizedCnpj.Substring(5, 3)}/{normalizedCnpj.Substring(8, 4)}-{normalizedCnpj.Substring(12, 2)}";
        }
    }
}