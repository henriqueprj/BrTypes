﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BrTypes
{
    public readonly struct Cnpj : IEquatable<Cnpj>
    {
        private const string Mask = "##.###.###/####-##";
        private const int CnpjLength = 14;
        
        private readonly long _value;

        private Cnpj(long value)
        {
            _value = value;
        }

        public static Cnpj Parse(string s)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            
            if (!TryParse(s, out var cnpj))
                throw new CnpjInvalidoException(s);    
                
            return cnpj;
        }
        
        /// <summary>
        /// Converte uma string em Cnpj e retorna o valor indicando se a conversão foi realizada com sucesso.
        /// </summary>
        /// <param name="s">Representação string do CNPJ</param>
        /// <param name="result">Estrutura de dados representando o CNPJ convertido</param>
        /// <returns>True caso a conversão tenha ocorrido com sucesso.</returns>
        public static bool TryParse([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[CnpjLength]; 

            if (!Digits.Default.TryParse(s, digits))
            {
                result = default;
                return false;
            }

            if (AllSame(digits))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV(digits.Slice(0, 12));
            
            if (dv != digits[12] * 10 + digits[13])
            {
                result = default;
                return false;
            }

            long number =
                digits[0]  * 10000000000000L +
                digits[1]  * 1000000000000L +
                digits[2]  * 100000000000L +
                digits[3]  * 10000000000L +
                digits[4]  * 1000000000L +
                digits[5]  * 100000000L +
                digits[6]  * 10000000L +
                digits[7]  * 1000000L +
                digits[8]  * 100000L +
                digits[9]  * 10000L +
                digits[10] * 1000L +
                digits[11] * 100L +
                digits[12] * 10L +
                digits[13];

            result = new Cnpj(number);
            return true;
        }

        public override string ToString()
        {
            return ToStringInternal(EstiloFormatacaoCnpj.Nenhum);
        }
        
        public string ToString(EstiloFormatacaoCnpj estilo)
        {
            return ToStringInternal(estilo);
        }

        private string ToStringInternal(EstiloFormatacaoCnpj estilo)
        {
            var value = _value;
            Span<char> digits = stackalloc char[CnpjLength];
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i] = (char)(value % 10 + '0');
                value /= 10;
            }

            return estilo switch
            {
                EstiloFormatacaoCnpj.Nenhum => digits.ToString(),
                EstiloFormatacaoCnpj.Padrao => Masks.Apply(Mask, digits),
                _ => throw new ArgumentException("O valor informado não é um estilo de formatação de CNPJ válido",
                    nameof(estilo))
            };

        }

        public bool Equals(Cnpj other)
        {
            return _value == other._value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Cnpj other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Cnpj left, Cnpj right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cnpj left, Cnpj right)
        {
            return !left.Equals(right);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV(ReadOnlySpan<int> digits)
        {
            var sum1 =
                digits[0] * 5 +
                digits[1] * 4 +
                digits[2] * 3 +
                digits[3] * 2 +
                digits[4] * 9 +
                digits[5] * 8 +
                digits[6] * 7 +
                digits[7] * 6 +
                digits[8] * 5 +
                digits[9] * 4 +
                digits[10] * 3 +
                digits[11] * 2;

            var sum2 =
                digits[0] * 6 +
                digits[1] * 5 +
                digits[2] * 4 +
                digits[3] * 3 +
                digits[4] * 2 +
                digits[5] * 9 +
                digits[6] * 8 +
                digits[7] * 7 +
                digits[8] * 6 +
                digits[9] * 5 +
                digits[10] * 4 +
                digits[11] * 3;

            var dv1 = Mod11(sum1);
            var dv2 = Mod11(sum2 + dv1 * 2);

            return dv1 * 10 + dv2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11(int sum)
        {
            var reminder = sum % 11;
            return reminder <= 2 ? 0 : 11 - reminder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AllSame(ReadOnlySpan<int> digits)
        {
            for (var i = 1; i < CnpjLength; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }

            return true;
        }
    }
}