namespace next_CNPJ.Core.Domain
{
    /// <summary>
    /// Enum que representa o formato do CNPJ.
    /// </summary>
    public enum CnpjFormat
    {
        /// <summary>
        /// CNPJ exclusivamente numérico (formato tradicional).
        /// </summary>
        Numeric,

        /// <summary>
        /// CNPJ alfanumérico (novo formato com letras na raiz ou ordem).
        /// </summary>
        Alphanumeric
    }
}
