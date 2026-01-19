using System.Linq;

namespace next_CNPJ.Core.Utilities
{
    /// <summary>
    /// Utilitário para cálculo e validação dos dígitos verificadores do CNPJ.
    /// Implementa o algoritmo módulo 11 com conversão ASCII.
    /// </summary>
    public static class DigitVerifierCalculator
    {
        private const int Modulo = 11;
        private const int MinWeight = 2;
        private const int MaxWeight = 9;
        private const int ExpectedLength = 14;
        private const int BaseLength = 12;

        /// <summary>
        /// Calcula o primeiro dígito verificador do CNPJ.
        /// </summary>
        /// <param name="cnpjBase">Primeiras 12 posições do CNPJ (raiz + ordem).</param>
        /// <returns>Primeiro dígito verificador (0-9).</returns>
        public static int CalculateFirstDigit(string cnpjBase)
        {
            if (string.IsNullOrEmpty(cnpjBase) || cnpjBase.Length != BaseLength)
                return -1;

            var sum = 0;
            var weight = MinWeight;

            // Calcula da direita para a esquerda (posições 12 até 1)
            for (int i = cnpjBase.Length - 1; i >= 0; i--)
            {
                var numericValue = AsciiConverter.ToNumericValue(cnpjBase[i]);
                sum += numericValue * weight;

                weight++;
                if (weight > MaxWeight)
                    weight = MinWeight;
            }

            var remainder = sum % Modulo;
            var digit = Modulo - remainder;

            // Se o resultado for 10 ou 11, o dígito é 0
            return digit >= 10 ? 0 : digit;
        }

        /// <summary>
        /// Calcula o segundo dígito verificador do CNPJ.
        /// </summary>
        /// <param name="cnpjBase">Primeiras 12 posições do CNPJ (raiz + ordem).</param>
        /// <param name="firstDigit">Primeiro dígito verificador já calculado.</param>
        /// <returns>Segundo dígito verificador (0-9).</returns>
        public static int CalculateSecondDigit(string cnpjBase, int firstDigit)
        {
            if (string.IsNullOrEmpty(cnpjBase) || cnpjBase.Length != BaseLength)
                return -1;

            // Concatena a base com o primeiro dígito para calcular o segundo
            var cnpjWithFirstDigit = cnpjBase + firstDigit.ToString();

            var sum = 0;
            var weight = MinWeight;

            // Calcula da direita para a esquerda (posições 13 até 1)
            for (int i = cnpjWithFirstDigit.Length - 1; i >= 0; i--)
            {
                var numericValue = AsciiConverter.ToNumericValue(cnpjWithFirstDigit[i]);
                sum += numericValue * weight;

                weight++;
                if (weight > MaxWeight)
                    weight = MinWeight;
            }

            var remainder = sum % Modulo;
            var digit = Modulo - remainder;

            // Se o resultado for 10 ou 11, o dígito é 0
            return digit >= 10 ? 0 : digit;
        }

        /// <summary>
        /// Valida os dígitos verificadores de um CNPJ completo (14 posições).
        /// </summary>
        /// <param name="cnpj">CNPJ completo com 14 posições.</param>
        /// <returns>True se os dígitos verificadores estão corretos.</returns>
        public static bool ValidateDigits(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || cnpj.Length != ExpectedLength)
                return false;

            var baseCnpj = cnpj.Substring(0, BaseLength);
            var firstDigitProvided = int.Parse(cnpj[BaseLength].ToString());
            var secondDigitProvided = int.Parse(cnpj[BaseLength + 1].ToString());

            var firstDigitCalculated = CalculateFirstDigit(baseCnpj);
            var secondDigitCalculated = CalculateSecondDigit(baseCnpj, firstDigitCalculated);

            return firstDigitCalculated == firstDigitProvided &&
                   secondDigitCalculated == secondDigitProvided;
        }
    }
}
