using System;
using System.Runtime.CompilerServices;

namespace BrTypes
{
    public readonly struct Cpf2 : IEquatable<Cpf2>
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
            
            Span<char> digits = stackalloc char[11];
            if (!Digits.TryParse(s, digits))
            {
                result = default;
                return false;
            }

            if (Digits.AllSame(in digits))
            {
                result = default;
                return false;
            }

            var dv = (digits[9] - '0') * 10 + (digits[10] - '0'); 
            var calculatedDV = CalculateDV(in digits);
            if (dv != calculatedDV)
            {
                result = default;
                return false;
            }
            
            var cpfBase = DigitsToInt32(in digits);
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

        public static implicit operator Cpf2(string numero) => Parse(numero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV(in Span<char> digits)
        {
            // this is on purpose for performance reasons
            var sum1 =
                (digits[0] - '0') * 1 +
                (digits[1] - '0') * 2 +
                (digits[2] - '0') * 3 +
                (digits[3] - '0') * 4 +
                (digits[4] - '0') * 5 +
                (digits[5] - '0') * 6 +
                (digits[6] - '0') * 7 +
                (digits[7] - '0') * 8 +
                (digits[8] - '0') * 9;

            // this is on purpose for performance reasons
            var sum2 =
                (digits[0] - '0') * 0 +
                (digits[1] - '0') * 1 +
                (digits[2] - '0') * 2 +
                (digits[3] - '0') * 3 +
                (digits[4] - '0') * 4 +
                (digits[5] - '0') * 5 +
                (digits[6] - '0') * 6 +
                (digits[7] - '0') * 7 +
                (digits[8] - '0') * 8;

            var dv1 = Mod11(sum1);
            var dv2 = Mod11(sum2 + (dv1 * 9));

            return dv1 * 10 + dv2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11(int value)
        {
            var result = value % 11;
            return result == 10 ? 0 : result;
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
                var baseDigits = digits.Slice(0, 9);
                Int32ToDigits(value, baseDigits);
                var dv = CalculateDV(in baseDigits);
                digits[9] = (char)(dv / 10 + '0');
                digits[10] = (char)(dv % 10 + '0');
#if NETCOREAPP2_1_OR_GREATER
            });
#else
                return digits.ToString();
#endif
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int DigitsToInt32(in Span<char> digits)
        {
            System.Diagnostics.Debug.Assert(digits.Length == 9, "digits should have length 9");
            
            // this is on purpose for performance reasons
            return (digits[0] - '0') * 100000000 +
                   (digits[1] - '0') * 10000000 +
                   (digits[2] - '0') * 1000000 +
                   (digits[3] - '0') * 100000 +
                   (digits[4] - '0') * 10000 +
                   (digits[5] - '0') * 1000 +
                   (digits[6] - '0') * 100 +
                   (digits[7] - '0') * 10 +
                   (digits[8] - '0');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Int32ToDigits(int baseCpf, Span<char> digits)
        {
            System.Diagnostics.Debug.Assert(baseCpf < 1_000_000_000, "baseCpf should be 9 (999999999) digits max");
            
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                baseCpf = Math.DivRem(baseCpf, 10, out var digit);
                digits[i] = (char)(digit + '0');
            }
        }

        public bool Equals(Cpf2 other) => _value == other._value;

        public override bool Equals(object? obj) => obj is Cpf2 other && Equals(other);

        public override int GetHashCode() => _value;

        public static bool operator ==(Cpf2 left, Cpf2 right) => left.Equals(right);

        public static bool operator !=(Cpf2 left, Cpf2 right) => !left.Equals(right);
    }
}