using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BrTypes
{
    public readonly struct Cpf2
    {
        private readonly int _value;
        private Cpf2(int cpfBase)
        {
            _value = cpfBase;
        }
        
        public static bool TryParse(string? s, out Cpf2 result)
        {
            if (s is null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[11];
            if (!Digits.TryParse(s, ref digits))
            {
                result = default;
                return false;
            }
            
            if (Digits.AllSame(digits.Slice(0, 9)))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV(in digits);
            if (digits[9] * 10 + digits[10] != dv)
            {
                result = default;
                return false;
            }

            var cpfBase =
                digits[0] * 100000000 +
                digits[1] * 10000000 +
                digits[2] * 1000000 +
                digits[3] * 100000 +
                digits[4] * 10000 +
                digits[5] * 1000 +
                digits[6] * 100 +
                digits[7] * 10 +
                digits[8];
            
            result = new Cpf2(cpfBase);
            return true;
        }
        
        public static Cpf2 Parse(string? s)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            
            if (!TryParse(s, out var cpfValido))
                throw new CpfInvalidoException(s);

            return cpfValido;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV(in Span<int> digits)
        {
            var sum1 =
                digits[0] * 10 +
                digits[1] * 9 +
                digits[2] * 8 +
                digits[3] * 7 +
                digits[4] * 6 +
                digits[5] * 5 +
                digits[6] * 4 +
                digits[7] * 3 +
                digits[8] * 2;
        
        
            var sum2 =
                digits[0] * 11 +
                digits[1] * 10 +
                digits[2] * 9 +
                digits[3] * 8 +
                digits[4] * 7 +
                digits[5] * 6 +
                digits[6] * 5 +
                digits[7] * 4 +
                digits[8] * 3;
        
            var dv1 = Digits.Mod11(sum1);
            var dv2 = Digits.Mod11(sum2 + dv1 * 2);
        
            return dv1 * 10 + dv2;
        }

        public override string ToString()
        {
#if NETCOREAPP2_1_OR_GREATER
            return string.Create(11, _value, (digits, state) =>
            {
                int value = state;
#else
                int value = _value;
                Span<char> digits = stackalloc char[11];
#endif
                Span<int> digitsToCalculateDV = stackalloc int[9];
                for (var i = 8; i >= 0; i--)
                {
                    var digit = value % 10;
                    value /= 10;
                    digits[i] = (char)(digit + '0');
                    digitsToCalculateDV[i] = digit;
                }
                var dv = CalculateDV(in digitsToCalculateDV);
                digits[9] = (char)(dv / 10 + '0');
                digits[10] = (char)(dv % 10 + '0');
#if NETCOREAPP2_1_OR_GREATER                
            });
#else
                return digits.ToString();
#endif
        }
    }
}