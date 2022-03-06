using System;
using System.Diagnostics.CodeAnalysis;
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
            if (!Digits.TryParse(s, ref digits))
            {
                result = default;
                return false;
            }

            if (Digits.AllSame(in digits))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV2(in digits);
            if ((digits[9] - '0') * 10 + (digits[10] - '0') != dv)
            {
                result = default;
                return false;
            }
            
            // this is on purpose for performance reasons
            var cpfBase =
                (digits[0] - '0') * 100000000 +
                (digits[1] - '0') * 10000000 +
                (digits[2] - '0') * 1000000 +
                (digits[3] - '0') * 100000 +
                (digits[4] - '0') * 10000 +
                (digits[5] - '0') * 1000 +
                (digits[6] - '0') * 100 +
                (digits[7] - '0') * 10 +
                (digits[8] - '0');

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
        private static int CalculateDV(in Span<int> digits)
        {
            // this is on purpose for performance reasons
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

            // this is on purpose for performance reasons
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV2(in Span<char> digits)
        {
            // this is on purpose for performance reasons
            var sum1 =
                (digits[0] - '0') * 10 +
                (digits[1] - '0') * 9 +
                (digits[2] - '0') * 8 +
                (digits[3] - '0') * 7 +
                (digits[4] - '0') * 6 +
                (digits[5] - '0') * 5 +
                (digits[6] - '0') * 4 +
                (digits[7] - '0') * 3 +
                (digits[8] - '0') * 2;

            // this is on purpose for performance reasons
            var sum2 =
                (digits[0] - '0') * 11 +
                (digits[1] - '0') * 10 +
                (digits[2] - '0') * 9 +
                (digits[3] - '0') * 8 +
                (digits[4] - '0') * 7 +
                (digits[5] - '0') * 6 +
                (digits[6] - '0') * 5 +
                (digits[7] - '0') * 4 +
                (digits[8] - '0') * 3;

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
                var baseDigits = digits.Slice(0, 9);
                Int32ToSpanOfChar(value, baseDigits);
                var dv = CalculateDV2(in baseDigits);
                digits[9] = (char)(dv / 10 + '0');
                digits[10] = (char)(dv % 10 + '0');
#if NETCOREAPP2_1_OR_GREATER
            });
#else
                return digits.ToString();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Int32ToSpanOfChar(int baseCpf, Span<char> digits)
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