using System;

namespace BrTypes
{
    public class CnpjInvalidoException : Exception
    {
        public string? Value { get; }

        public CnpjInvalidoException(string? value)
        {
            Value = value;
        }

        public override string Message => $"O valor informado ('{Value}') não é um CNPJ válido";
    }
}