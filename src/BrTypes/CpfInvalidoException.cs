using System;

namespace BrTypes
{
    
    /// <summary>
    /// Exception lançada quando <see cref="Cpf"/> é instanciado com um CPF inválido.
    /// </summary>
    public sealed class CpfInvalidoException : Exception
    {
        /// <summary>
        /// Retorna o número informado na validação do CPF.
        /// </summary>
        public string Numero { get; }

        public CpfInvalidoException(string numero)
        {
            Numero = numero;
        }

        public override string Message => $"'{Numero}' não é um CPF válido";
    }
}