using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BrTypes
{
    public static class Digits
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParse([NotNullWhen(true)]string? s, ref Span<int> digits)
        {
            if (s is null)
                return false;
            
            var digitIndex = 0;
        
            for (var i = 0; i < s!.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit is < 0 or > 9)
                    continue;
        
                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = digit;
                digitIndex++;
            }
            
            return digitIndex == digits.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParse([NotNullWhen(true)]string? s, Span<char> digits)
        {
            if (s is null)
                return false;
            
            var digitIndex = 0;

            for (var i = 0; i < s!.Length; i++)
            {
                if (s[i] is < '0' or > '9')
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = s[i];
                digitIndex++;
            }
            
            return digitIndex == digits.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSame(in Span<char> digits)
        {
            if (digits.Length == 0) return false;

            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
    }
}