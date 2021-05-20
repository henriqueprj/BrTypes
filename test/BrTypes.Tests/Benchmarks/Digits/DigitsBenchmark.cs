using System;
using BenchmarkDotNet.Attributes;

namespace BrTypes.Tests.Benchmarks.Digits
{
    [MemoryDiagnoser]
    public class DigitsBenchmark
    {
        
        [Benchmark(Baseline = true)]
        public bool Digits_Fill()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill(digits, "07.245.465/0001-51");
        }

        [Benchmark]
        public bool Digits_Fill12()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill12(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill13()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill13(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill2()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill2(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill3()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill3(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill31()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill31(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill32()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill32(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill4()
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill4(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill41()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill41(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill42()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill42(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill43()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill43(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill44()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.Digits.Fill44(digits, "07.245.465/0001-51");
        }
    }
}