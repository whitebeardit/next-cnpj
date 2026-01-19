using System.Linq;

namespace next_CNPJ.Core.Domain
{
    /// <summary>
    /// Configuração para validação de CNPJ.
    /// </summary>
    public class CnpjConfiguration
    {
        /// <summary>
        /// Letras que não devem ser aceitas no CNPJ alfanumérico.
        /// Padrão: I, O, U, Q, F (conforme solicitação técnica do ENCAT).
        /// </summary>
        public char[] ExcludedLetters { get; set; }

        /// <summary>
        /// Flag para permitir letras excluídas mesmo que estejam na lista.
        /// Padrão: false (não permite letras excluídas).
        /// </summary>
        public bool AllowExcludedLetters { get; set; }

        /// <summary>
        /// Cria uma instância com configuração padrão.
        /// </summary>
        public CnpjConfiguration()
        {
            ExcludedLetters = new[] { 'I', 'O', 'U', 'Q', 'F' };
            AllowExcludedLetters = false;
        }

        /// <summary>
        /// Verifica se uma letra está na lista de excluídas.
        /// </summary>
        /// <param name="letter">Letra a verificar.</param>
        /// <returns>True se a letra está excluída e não é permitida.</returns>
        public bool IsLetterExcluded(char letter)
        {
            if (AllowExcludedLetters)
                return false;

            return ExcludedLetters.Contains(char.ToUpperInvariant(letter));
        }
    }
}
