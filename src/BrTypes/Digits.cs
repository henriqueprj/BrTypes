﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BrTypes
{
    public static class DigitZ
    {
        public static bool Fill(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit is < 0 or > 9)
                    continue;

                digits[digitIndex] = digit;
                digitIndex = digitIndex + 1;
            }

            // Parsed more or less than 14 digits?
            return digitIndex == digits.Length;
        }

        public static bool Fill12(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit is < 0 or > 9)
                    continue;

                digits[digitIndex] = digit;
                digitIndex += 1;
            }
            
            return digitIndex == digits.Length;
        }
        
        public static bool Fill13(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit is < 0 or > 9)
                    continue;

                digits[digitIndex++] = digit;
            }

            // Parsed more or less than 14 digits?
            return digitIndex == digits.Length;
        }
        
        public static bool Fill2(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                var increment = digit is < 0 or > 9 ? 0 : 1;

                digits[digitIndex] = digit;
                
                digitIndex += increment;
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill3(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                digits[digitIndex] = digit;
                var increment = digit is < 0 or > 9 ? 0 : 1;
                digitIndex = digitIndex + increment;
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill31(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                digits[digitIndex] = digit;
                digitIndex = digitIndex + (digit is < 0 or > 9 ? 0 : 1);
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill32(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                digits[digitIndex] = s[i] - '0';
                digitIndex = digitIndex + (digits[digitIndex] is < 0 or > 9 ? 0 : 1);
            }

            return digitIndex == digits.Length;
        }

        public static unsafe bool Fill4(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                digits[digitIndex] = digit;
                var isDigit = digit is >= 0 and <= 9;
                digitIndex += *(byte*)(&isDigit);
            }

            return digitIndex == digits.Length;
        }
        
        public static unsafe bool Fill41(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                digits[digitIndex] = digit;
                var isDigit = digit is >= 0 and <= 9;
                digitIndex = digitIndex + *(byte*)(&isDigit);
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill42(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                digits[digitIndex] = digit;
                digitIndex += Convert.ToInt32(digit is >= 0 and <= 9);
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill43(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                var digit = s[i] - '0';
                digits[digitIndex] = digit;
                digitIndex = digitIndex + Convert.ToInt32(digit is >= 0 and <= 9);
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill44(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length && digitIndex < digits.Length; i++)
            {
                digits[digitIndex] = s[i] - '0';
                digitIndex = digitIndex + Convert.ToInt32(digits[digitIndex] is >= 0 and <= 9);
            }

            return digitIndex == digits.Length;
        }
        
        public static bool Fill5(Span<int> digits, string s)
        {
            var digitIndex = 0;

            foreach (var c in s)
            {
                var digit = c - '0';
                if (digit is < 0 or > 9)
                    continue;
                
                digits[digitIndex] = digit;
                digitIndex++;
            }
            
            return digitIndex == digits.Length;
        }
        
        public static bool Fill51(Span<int> digits, string s)
        {
            var digitIndex = 0;

            foreach (var c in s)
            {
                var digit = c - '0';
                if (digit is < 0 or > 9)
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = digit;
                digitIndex++;
            }
            
            return digitIndex == digits.Length;
        }
        
        public static bool Fill52(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length; i++)
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
        
        public static bool Fill53(Span<int> digits, string s)
        {
            var digitIndex = 0;

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                var digit = c - '0';
                if (digit is < 0 or > 9)
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = digit;
                digitIndex++;
            }
            
            return digitIndex == digits.Length;
        }
        
        public static bool Fill54(Span<int> digits, string s)
        {
            var digitIndex = 0;

            foreach (var c in s)
            {
                var digit = c - '0';
                if (digit is < 0 or > 9)
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = digit;
                digitIndex = digitIndex + 1;
            }
            
            return digitIndex == digits.Length;
        }
        
        public static bool Fill55(Span<int> digits, string s)
        {
            var digitIndex = 0;

            foreach (var c in s)
            {
                var digit = c - '0';
                if (digit is < 0 or > 9)
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = digit;
                digitIndex += 1;
            }
            
            return digitIndex == digits.Length;
        }
        
        public static bool Fill56(Span<int> digits, string s)
        {
            var digitIndex = 0;

            foreach (var c in s)
            {
                var digit = c - '0';
                if (digit is < 0 or > 9)
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex++] = digit;
            }
            
            return digitIndex == digits.Length;
        }
        

        
    }

    public class Digits// : IDigitsFiller
    {
        //public static readonly Digits Default = new();

        // private Digits()
        // {
        // }
        
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
        public static bool TryParse([NotNullWhen(true)]string? s, ref Span<byte> digits)
        {
            if (s is null)
                return false;
            
            var digitIndex = 0;

            for (var i = 0; i < s.Length; i++)
            {
                var digit = s[i] - '0';
                if (digit is < 0 or > 9)
                    continue;

                if (digitIndex == digits.Length)
                    return false;
                
                digits[digitIndex] = (byte)digit;
                digitIndex++;
            }
            
            return digitIndex == digits.Length;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSame(in ReadOnlySpan<int> digits)
        {
            if (digits.Length == 0) return false;

            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSame(in ReadOnlySpan<byte> digits)
        {
            if (digits.Length == 0) return false;

            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSame(in Span<byte> digits)
        {
            if (digits.Length == 0) return false;

            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSame(in Span<int> digits)
        {
            if (digits.Length == 0) 
                return false;

            for (var i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] != digits[i])
                    return false;
            }
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Mod11(int value)
        {
            var result = 11 - value % 11;
            return result < 10 ? result : 0;
        }
    }
}