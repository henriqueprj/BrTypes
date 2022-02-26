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

        public static bool TryParse0([NotNullWhen(true)]string? s, out Cpf2 result)
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
                if (n is < 0 or > 9)
                    continue;
                if (position > 10)
                {
                    result = default;
                    return false;
                }
                digits[position] = n;
                position++;
            }

            if (Digits.AllSame(in digits))
            {
                result = default;
                return false;
            }

            var baseCpf = digits.Slice(0, 9);
            var dv = CalculateDV(baseCpf);
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

            var dv = CalculateDV3(in digits);
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
        
        public static bool TryParse55(string? s, out Cpf2 result)
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

            var dv = CalculateDV3(in digits);
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

        public static bool TryParse2(string? s, out Cpf2 result)
        {
            if (s is null)
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
            
            if (position != digits.Length)
            {
                result = default;
                return false;
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
        
        public static bool TryParse3(string? s, out Cpf2 result)
        {
            if (s is null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[11];
            var position = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var n = s[i] - '0';
                if (n is < 0 or > 9)
                    continue;

                if (position == digits.Length)
                {
                    result = default;
                    return false;
                }
                
                digits[position] = n;
                position++;
            }

            if (position != digits.Length)
            {
                result = default;
                return false;
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
        
        public static bool TryParse4(string? s, out Cpf2 result)
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
        
        
        
        public static bool TryParse6(string? s, out Cpf2 result)
        {
            if (s is null)
            {
                result = default;
                return false;
            }
            
            Span<byte> digits = stackalloc byte[11];
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

            var dv = CalculateDV3(digits);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AllSameDigits(in Span<int> digits)
        {
            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AllSameDigits2(in Span<int> digits)
        {
            var allSame = true;
            for (var i = 1; i < digits.Length; i++)
            {
                allSame &= digits[i - 1] != digits[i];
            }
            return allSame;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AllSameDigits(Span<byte> digits)
        {
            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV3(ReadOnlySpan<int> digits)
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
        
            var dv1 = Mod11(sum1);
            var dv2 = Mod11(sum2 + dv1 * 2);
        
            return dv1 * 10 + dv2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV3(Span<byte> digits)
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
        
            var dv1 = Mod11(sum1);
            var dv2 = Mod11(sum2 + dv1 * 2);
        
            return dv1 * 10 + dv2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV3(in Span<int> digits)
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