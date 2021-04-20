using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace BrTypes
{
    public readonly struct Cpf2
    {
        private readonly int _value;
        private Cpf2(int cpfBase)
        {
            _value = cpfBase;
        }

        public static bool TryParse([NotNullWhen(true)]string? s, out Cpf2 result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[11];
            var position = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var n = s[i] - '0';
                if (n < 0 || n > 9)
                    continue;
                if (position > 10)
                {
                    result = default;
                    return false;
                }
                digits[position] = n;
                position++;
            }

            if (AllSameDigits(digits))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV(digits.Slice(0, 9));
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

        public static bool TryParse2(string s, out Cpf2 result)
        {
            Span<int> digits = stackalloc int[11];
            var position = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var n = s[i] - '0';
                if (n < 0 || n > 9)
                    continue;
                // if (position > 10)
                // {
                //     result = default;
                //     return false;
                // }
                digits[position] = n;
                position++;
            }

            if (AllSameDigits(digits))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV(digits.Slice(0, 9));
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
        
        public static bool TryParse3(string s, out Cpf2 result)
        {
            Span<int> digits = stackalloc int[11];
            var position = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var n = s[i] - '0';
                if (n < 0 || n > 9)
                    continue;
                // if (position > 10)
                // {
                //     result = default;
                //     return false;
                // }
                digits[position] = n;
                position++;
            }

            if (AllSameDigits(digits))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV2(digits.Slice(0, 9));
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

        private static bool AllSameDigits(Span<int> digits)
        {
            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        private static int CalculateDV(Span<int> cpf)
        {
            var s1 = 0;
            var s2 = 0;
            for (int i = 0, multiplier = 10; i < cpf.Length; i++, multiplier--)
            {
                var n = cpf[i];
                s1 += n * multiplier;
                s2 += n * (multiplier + 1);
            }
            int r;
            r = s1 % 11;
            var dv0 = r >= 2 ? 11 - r : 0;
            r = (s2 + (dv0 * 2)) % 11;
            var dv1 = r >= 2 ? 11 - r : 0;
            var dv = dv0 * 10 + dv1;
            return dv;
        }
        
        private static int CalculateDV2(Span<int> cpf)
        {
            var s1 = 0;
            var s2 = 0;
            for (int i = 0, multiplier = 10; i < cpf.Length; i++, multiplier--)
            {
                s1 += cpf[i] * multiplier;
                s2 += cpf[i] * (multiplier + 1);
            }
            
            int r;
            r = s1 % 11;
            var dv0 = r >= 2 ? 11 - r : 0;
            
            r = (s2 + (dv0 * 2)) % 11;
            var dv1 = r >= 2 ? 11 - r : 0;
            var dv = dv0 * 10 + dv1;
            
            return dv;
        }


        public override string ToString()
        {
            var value = _value;
            Span<char> digits = stackalloc char[11];
            Span<int> digitsToDV = stackalloc int[9];
            for (var i = 8; i >= 0; i--)
            {
                var digit = value % 10;
                value /= 10;
                digits[i] = (char)(digit + '0');
                digitsToDV[i] = digit;
            }
            var dv = CalculateDV(digitsToDV);
            digits[9] = (char)(dv / 10 + '0');
            digits[10] = (char)(dv % 10 + '0');
            return digits.ToString();
        }
    }
}