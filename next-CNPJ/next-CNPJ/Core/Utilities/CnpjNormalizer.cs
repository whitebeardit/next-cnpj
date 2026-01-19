using System.Linq;
using System.Text;

namespace next_CNPJ.Core.Utilities
{
    /// <summary>
    /// Utilitário para normalização de CNPJ.
    /// Remove formatação e converte letras para maiúsculas.
    /// </summary>
    public static class CnpjNormalizer
    {
        /// <summary>
        /// Normaliza um CNPJ removendo formatação e convertendo para maiúsculas.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser normalizado.</param>
        /// <returns>CNPJ normalizado (apenas caracteres alfanuméricos em maiúsculas).</returns>
        public static string Normalize(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return string.Empty;

            var normalized = new StringBuilder();
            
            foreach (var character in cnpj!)
            {
                if (char.IsLetterOrDigit(character))
                {
                    normalized.Append(char.ToUpperInvariant(character));
                }
            }

            return normalized.ToString();
        }
    }
}
