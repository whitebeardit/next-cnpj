namespace next_CNPJ.Core.Utilities
{
    /// <summary>
    /// Utilitário para conversão de caracteres para valores numéricos usando tabela ASCII.
    /// Converte conforme regra: valor ASCII - 48.
    /// </summary>
    public static class AsciiConverter
    {
        /// <summary>
        /// Converte um caractere para seu valor numérico baseado na tabela ASCII.
        /// Fórmula: ASCII value - 48
        /// </summary>
        /// <param name="character">Caractere a ser convertido (numérico ou alfabético).</param>
        /// <returns>Valor numérico correspondente.</returns>
        /// <remarks>
        /// Caracteres numéricos (0-9) mantêm seus valores (0-9).
        /// Caracteres alfabéticos assumem valores específicos (A=17, B=18, C=19, etc.).
        /// </remarks>
        public static int ToNumericValue(char character)
        {
            return character - 48;
        }
    }
}
