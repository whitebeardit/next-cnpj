using next_CNPJ.Core.Domain;

namespace next_CNPJ.Core.Services
{
    /// <summary>
    /// Interface para identificação do formato de CNPJ (numérico ou alfanumérico).
    /// </summary>
    public interface ICnpjFormatIdentifier
    {
        /// <summary>
        /// Identifica o formato do CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser analisado (pode conter formatação).</param>
        /// <returns>Formato identificado (Numeric ou Alphanumeric).</returns>
        CnpjFormat IdentifyFormat(string? cnpj);

        /// <summary>
        /// Verifica se o CNPJ é alfanumérico.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser analisado (pode conter formatação).</param>
        /// <returns>True se o CNPJ contém letras na raiz ou ordem.</returns>
        bool IsAlphanumeric(string? cnpj);

        /// <summary>
        /// Verifica se o CNPJ é exclusivamente numérico.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser analisado (pode conter formatação).</param>
        /// <returns>True se o CNPJ contém apenas números.</returns>
        bool IsNumeric(string? cnpj);
    }
}
