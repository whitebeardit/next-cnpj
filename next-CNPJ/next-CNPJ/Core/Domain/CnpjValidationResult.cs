namespace next_CNPJ.Core.Domain
{
    /// <summary>
    /// Representa o resultado da validação de um CNPJ.
    /// </summary>
    public class CnpjValidationResult
    {
        /// <summary>
        /// Indica se o CNPJ é válido.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Formato identificado do CNPJ (Numérico ou Alfanumérico).
        /// </summary>
        public CnpjFormat Format { get; }

        /// <summary>
        /// Mensagem de erro caso a validação falhe. Null se válido.
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// CNPJ normalizado (sem formatação, apenas caracteres).
        /// </summary>
        public string NormalizedCnpj { get; }

        /// <summary>
        /// Cria uma instância de resultado válido.
        /// </summary>
        /// <param name="format">Formato do CNPJ.</param>
        /// <param name="normalizedCnpj">CNPJ normalizado.</param>
        public CnpjValidationResult(CnpjFormat format, string normalizedCnpj)
        {
            IsValid = true;
            Format = format;
            ErrorMessage = null;
            NormalizedCnpj = normalizedCnpj;
        }

        /// <summary>
        /// Cria uma instância de resultado inválido.
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro.</param>
        /// <param name="normalizedCnpj">CNPJ normalizado (pode estar vazio se a normalização falhou).</param>
        /// <param name="format">Formato identificado, se houver.</param>
        public CnpjValidationResult(string errorMessage, string normalizedCnpj = "", CnpjFormat format = CnpjFormat.Numeric)
        {
            IsValid = false;
            Format = format;
            ErrorMessage = errorMessage;
            NormalizedCnpj = normalizedCnpj;
        }
    }
}
