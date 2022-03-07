using System;
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

            if (!Digits.TryParse(s, ref digits))
            {
                result = default;
                return false;
            }

            if (AllSameDigits(digits))
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
        
        /// <summary>
        /// Converte uma string em Cnpj e retorna o valor indicando se a conversão foi realizada com sucesso.
        /// </summary>
        /// <param name="s">Representação string do CNPJ</param>
        /// <param name="result">Estrutura de dados representando o CNPJ convertido</param>
        /// <returns>True caso a conversão tenha ocorrido com sucesso.</returns>
        public static bool TryParse2([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<char> digits = stackalloc char[CnpjLength]; 

            if (!Digits.TryParse(s, digits))
            {
                result = default;
                return false;
            }

            if (Digits.AllSame(digits))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV2(digits);
            
            if (dv != (digits[12] * 10 + digits[13]))
            {
                result = default;
                return false;
            }

            long number =
                (digits[0] - '0')  * 10000000000000L +
                (digits[1] - '0')  * 1000000000000L +
                (digits[2] - '0')  * 100000000000L +
                (digits[3] - '0')  * 10000000000L +
                (digits[4] - '0')  * 1000000000L +
                (digits[5] - '0')  * 100000000L +
                (digits[6] - '0')  * 10000000L +
                (digits[7] - '0')  * 1000000L +
                (digits[8] - '0')  * 100000L +
                (digits[9] - '0')  * 10000L +
                (digits[10] - '0') * 1000L +
                (digits[11] - '0') * 100L +
                (digits[12] - '0') * 10L +
                (digits[13] - '0');

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
        private static int CalculateDV2(Span<char> digits)
        {
            var sum1 =
                (digits[0] - '0') * 5 +
                (digits[1] - '0') * 4 +
                (digits[2] - '0') * 3 +
                (digits[3] - '0') * 2 +
                (digits[4] - '0') * 9 +
                (digits[5] - '0') * 8 +
                (digits[6] - '0') * 7 +
                (digits[7] - '0') * 6 +
                (digits[8] - '0') * 5 +
                (digits[9] - '0') * 4 +
                (digits[10] - '0') * 3 +
                (digits[11] - '0') * 2;

            var sum2 =
                (digits[0] - '0') * 6 +
                (digits[1] - '0') * 5 +
                (digits[2] - '0') * 4 +
                (digits[3] - '0') * 3 +
                (digits[4] - '0') * 2 +
                (digits[5] - '0') * 9 +
                (digits[6] - '0') * 8 +
                (digits[7] - '0') * 7 +
                (digits[8] - '0') * 6 +
                (digits[9] - '0') * 5 +
                (digits[10] - '0') * 4 +
                (digits[11] - '0') * 3;

            var dv1 = Mod11(sum1);
            var dv2 = Mod11(sum2 + dv1 * 2);

            return dv1 * 10 + dv2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11(int value)
        {
            var result = 11 - value % 11;
            return result < 10 ? result : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AllSameDigits(ReadOnlySpan<int> digits)
        {
            switch (digits.Length)
            {
                case 0:
                    return false;
                case 1:
                    return true;
            }
            
            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }

            return true;
        }
    }
}