using next_CNPJ.Core.Domain;

namespace next_CNPJ.Core.Services
{
    /// <summary>
    /// Interface para validação de CNPJ.
    /// </summary>
    public interface ICnpjValidator
    {
        /// <summary>
        /// Valida um CNPJ conforme as regras da NTC 2025.001.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado (pode conter formatação).</param>
        /// <param name="config">Configuração opcional para validação. Se null, usa configuração padrão.</param>
        /// <returns>Resultado da validação com informações detalhadas.</returns>
        CnpjValidationResult Validate(string? cnpj, CnpjConfiguration? config = null);

        /// <summary>
        /// Verifica se um CNPJ é válido.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado (pode conter formatação).</param>
        /// <param name="config">Configuração opcional para validação. Se null, usa configuração padrão.</param>
        /// <returns>True se o CNPJ é válido.</returns>
        bool IsValid(string? cnpj, CnpjConfiguration? config = null);
    }
}
