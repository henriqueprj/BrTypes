using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BrTypes.Tests.Benchmarks.Digits
{
    [MemoryDiagnoser()]
    public class CalculateDVBenchmark
    {
        [Benchmark(Baseline = true)]
        public int CalculateDV()
        {
            Span<int> intSpan = stackalloc int[9];
            intSpan[0] = 1;
            intSpan[1] = 2;
            intSpan[2] = 3;
            intSpan[3] = 4;
            intSpan[4] = 5;
            intSpan[5] = 6;
            intSpan[6] = 7;
            intSpan[7] = 8;
            intSpan[8] = 9;

            return CalculateDV(intSpan);
        }
        
        [Benchmark]
        public int CalculateDV2()
        {
            Span<char> charSpan = stackalloc char[9];
            charSpan[0] = '1';
            charSpan[1] = '2';
            charSpan[2] = '3';
            charSpan[3] = '4';
            charSpan[4] = '5';
            charSpan[5] = '6';
            charSpan[6] = '7';
            charSpan[7] = '8';
            charSpan[8] = '9';

            return CalculateDV2(charSpan);
        }
        
        [Benchmark]
        public int CalculateDV3()
        {
            Span<char> charSpan = stackalloc char[9];
            charSpan[0] = '1';
            charSpan[1] = '2';
            charSpan[2] = '3';
            charSpan[3] = '4';
            charSpan[4] = '5';
            charSpan[5] = '6';
            charSpan[6] = '7';
            charSpan[7] = '8';
            charSpan[8] = '9';

            return CalculateDV3(charSpan);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV(ReadOnlySpan<int> digits)
        {
            // for max performance
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

            // for max performance
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

            var dv1 = BrTypes.Digits.Mod11(sum1);
            var dv2 = BrTypes.Digits.Mod11(sum2 + dv1 * 2);

            return dv1 * 10 + dv2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV2(ReadOnlySpan<char> digits)
        {
            // for max performance
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

            // for max performance
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

            var dv1 = BrTypes.Digits.Mod11(sum1);
            var dv2 = BrTypes.Digits.Mod11(sum2 + dv1 * 2);

            return dv1 * 10 + dv2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateDV3(ReadOnlySpan<char> digits)
        {
            int sum1 = 0, sum2 = 0;
            for (int i = 0, multiplier = 10; i < 9; i++, multiplier--)
            {
                var digit = digits[i] - '0'; 
                sum1 += digit * multiplier;
                sum2 += digit * (multiplier + 1);
            }
            
            var dv1 = BrTypes.Digits.Mod11(sum1);
            var dv2 = BrTypes.Digits.Mod11(sum2 + dv1 * 2);

            return dv1 * 10 + dv2;
        }
    }
}