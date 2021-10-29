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
            return BrTypes.DigitZ.Fill(digits, "07.245.465/0001-51");
        }

        // [Benchmark]
        // public bool Digits_Fill12()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill12(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill13()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill13(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill2()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill2(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill3()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill3(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill31()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill31(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill32()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill32(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill4()
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill4(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill41()
        //
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill41(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill42()
        //
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill42(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill43()
        //
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill43(digits, "07.245.465/0001-51");
        // }
        //
        // [Benchmark]
        // public bool Digits_Fill44()
        //
        // {
        //     Span<int> digits = stackalloc int[14];
        //     return BrTypes.Digits.Fill44(digits, "07.245.465/0001-51");
        // }
        
        [Benchmark]
        public bool Digits_Fill5()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill5(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill51()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill51(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill52()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill52(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill53()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill53(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill54()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill54(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill55()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill55(digits, "07.245.465/0001-51");
        }
        
        [Benchmark]
        public bool Digits_Fill56()
        
        {
            Span<int> digits = stackalloc int[14];
            return BrTypes.DigitZ.Fill56(digits, "07.245.465/0001-51");
        }
    }
}