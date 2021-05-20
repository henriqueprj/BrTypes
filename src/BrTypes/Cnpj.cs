using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BrTypes
{
    public readonly struct Cnpj : IEquatable<Cnpj>
    {
        private static readonly int[] Multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private const int CnpjLength = 14;

        private readonly long _number;

        public static readonly Cnpj Empty = default;
        
        private Cnpj(long number)
        {
            _number = number;
        }
        
        public static Cnpj Parse(string? s)
        {
            if (TryParse(s, out var cnpj))
                return cnpj;

            throw new CnpjInvalidoException(s);
        }

        public static bool TryParse([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[CnpjLength + 1]; // WTF? 15? Isn't CNPJ a 14 digits length? Yes, one more 
                                                               // for length check.
            var digitIndex = -1;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit < 0 || digit > 9)
                    continue;

                digits[++digitIndex] = digit;
            }

            // Parsed more or less than 14 digits?
            if (digitIndex != CnpjLength - 1)
            {
                result = default;
                return false;
            }
            
            if (AllSame(digits.Slice(0, 14)))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV(digits.Slice(0, 12));
            
            if (dv != (digits[12] * 10 + digits[13]))
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
        
        public static bool TryParse2([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[CnpjLength]; // WTF? 15? Isn't CNPJ a 14 digits length? Yes, one more 
            // for length check.
            var digitIndex = -1;
            var s1 = 0;
            var s2 = 0;
            //var allSame = true;
            
            for (var i = 0; i < s.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit < 0 || digit > 9)
                    continue;
                
                ++digitIndex;

                if (digitIndex < 12)
                {
                    s1 += digit * Multiplier1[digitIndex];
                    s2 += digit * Multiplier2[digitIndex];    
                }
                else if (digitIndex >= CnpjLength)
                {
                    result = default;
                    return false;
                }

                digits[digitIndex] = digit;
            }

            // Parsed more or less than 14 digits?
            if (digitIndex != CnpjLength - 1)
            {
                result = default;
                return false;
            }
            
            if (AllSame(digits))
            {
                result = default;
                return false;
            }

            var dv1 = Mod11(s1);
            var dv2 = Mod11(s2 + (dv1 * 2));
            
            if (dv1 != digits[12] || dv2 != digits[13])
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

        private static readonly int[] W1 = { 5, 4, 3, 2, 9, 8, 7, 6 };
        private static readonly int[] W2 = { 6, 5, 4, 3, 2, 9, 8, 7 };
        
        public static bool TryParse3([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[CnpjLength]; // WTF? 15? Isn't CNPJ a 14 digits length? Yes, one more 
            // for length check.
            var digitIndex = -1;
            var s1 = 0;
            var s2 = 0;
            //var allSame = true;
            //var multiplier = 5;
            
            for (var i = 0; i < s.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit < 0 || digit > 9)
                    continue;
                
                ++digitIndex;

                if (digitIndex < 12)
                {
                    // https://stackoverflow.com/a/11040718/246644
                    // Fastest modulus then denominator is power of 2 (this case)
                    s1 += digit * W2[(digitIndex + 1) & 7]; // 7 because of (W2.Length - 1) equals 7 in this case
                    s2 += digit * W2[digitIndex & 7];
                    
                    // s1 += digit * multiplier;
                    // s2 += digit * (multiplier == 9 ? 2 : multiplier + 1);
                    //
                    // multiplier = multiplier > 2 ? multiplier - 1 : 9;
                }
                else if (digitIndex >= CnpjLength)
                {
                    result = default;
                    return false;
                }

                digits[digitIndex] = digit;
            }

            // Parsed more or less than 14 digits?
            if (digitIndex != CnpjLength - 1)
            {
                result = default;
                return false;
            }
            
            if (AllSame(digits))
            {
                result = default;
                return false;
            }

            var dv1 = Mod11(s1);
            var dv2 = Mod11(s2 + (dv1 * 2));
            
            if (dv1 != digits[12] || dv2 != digits[13])
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
        
        public static bool TryParse4([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[CnpjLength]; // WTF? 15? Isn't CNPJ a 14 digits length? Yes, one more 
            // for length check.
            var digitIndex = -1;
            var s1 = 0;
            var s2 = 0;
            var multiplier = 5;
            
            for (var i = 0; i < s.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit < 0 || digit > 9)
                    continue;
                
                ++digitIndex;

                if (digitIndex < 12)
                {
                    s1 += digit * multiplier;
                    s2 += digit * (multiplier == 9 ? 2 : multiplier + 1);
                    
                    multiplier = multiplier > 2 ? multiplier - 1 : 9;
                }
                else if (digitIndex >= CnpjLength)
                {
                    result = default;
                    return false;
                }

                digits[digitIndex] = digit;
            }

            // Parsed more or less than 14 digits?
            if (digitIndex != CnpjLength - 1)
            {
                result = default;
                return false;
            }
            
            if (AllSame(digits))
            {
                result = default;
                return false;
            }

            var dv1 = Mod11(s1);
            var dv2 = Mod11(s2 + (dv1 * 2));
            
            if (dv1 != digits[12] || dv2 != digits[13])
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
        
        public static bool TryParse5([NotNullWhen(true)]string? s, out Cnpj result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }
            
            Span<int> digits = stackalloc int[CnpjLength + 1]; // WTF? 15? Isn't CNPJ a 14 digits length? Yes, one more 
            // for length check.
            var digitIndex = -1;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                // if (digit < 0 || digit > 9)
                //     continue;
                
                var increment = digit < 0 || digit > 9 ? 0 : 1;
                digitIndex += increment;

                digits[digitIndex] = digit;
            }

            // Parsed more or less than 14 digits?
            if (digitIndex != CnpjLength - 1)
            {
                result = default;
                return false;
            }
            
            if (AllSame(digits.Slice(0, 14)))
            {
                result = default;
                return false;
            }

            var dv = CalculateDV(digits.Slice(0, 12));
            
            if (dv != (digits[12] * 10 + digits[13]))
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV(in Span<int> digits)
        {
            var sum1 = 0;
            var sum2 = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                sum1 += digits[i] * Multiplier1[i];
                sum2 += digits[i] * Multiplier2[i];
            }
            
            var dv1 = Mod11(sum1);
            var dv2 = Mod11(sum2 + (dv1 * 2));

            return dv1 * 10 + dv2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11(int sum)
        {
            var reminder = sum % 11;
            return reminder <= 2 ? 0 : 11 - reminder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AllSame(in Span<int> digits)
        {
            for (var i = 1; i < CnpjLength; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }

            return true;
        }

        public bool Equals(Cnpj other)
        {
            return _number == other._number;
        }

        public override bool Equals(object? obj)
        {
            return obj is Cnpj other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _number.GetHashCode();
        }

        public static bool operator ==(Cnpj left, Cnpj right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cnpj left, Cnpj right)
        {
            return !left.Equals(right);
        }
    }
}