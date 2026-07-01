using System.Linq;

namespace next_CNPJ.Core.Domain
{
    /// <summary>
    /// Configuração para validação de CNPJ.
    /// </summary>
    public class CnpjConfiguration
    {
        /// <summary>
        /// Letras historicamente listadas na NT como dependentes de confirmação pela Receita Federal.
        /// Mantida por compatibilidade, mas o validador padrão aceita qualquer letra A-Z nas 12 primeiras posições.
        /// </summary>
        public char[] ExcludedLetters { get; set; }

        /// <summary>
        /// Flag mantida por compatibilidade para consumidores que usam <see cref="IsLetterExcluded(char)"/> diretamente.
        /// O validador padrão não rejeita letras com base nesta configuração.
        /// </summary>
        public bool AllowExcludedLetters { get; set; }

        /// <summary>
        /// Habilita a validação opcional das letras historicamente excluídas pela NT.
        /// Padrão: false, alinhado ao comportamento do validador oficial da Receita Federal.
        /// </summary>
        public bool ValidateExcludedLetters { get; set; }

        /// <summary>
        /// Cria uma instância com configuração padrão.
        /// </summary>
        public CnpjConfiguration()
        {
            ExcludedLetters = new[] { 'I', 'O', 'U', 'Q', 'F' };
            AllowExcludedLetters = false;
            ValidateExcludedLetters = false;
        }

        /// <summary>
        /// Verifica se uma letra está na lista histórica de letras excluídas.
        /// </summary>
        /// <param name="letter">Letra a verificar.</param>
        /// <returns>True se a letra está na lista histórica de exclusão e <see cref="AllowExcludedLetters"/> é false.</returns>
        public bool IsLetterExcluded(char letter)
        {
            if (AllowExcludedLetters)
                return false;

            return ExcludedLetters.Contains(char.ToUpperInvariant(letter));
        }
    }
}
