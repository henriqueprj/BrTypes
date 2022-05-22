using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BrTypes.Tests.Benchmarks.Digits
{
    [MemoryDiagnoser]
    public class ModBenchmark
    {
        private static readonly int[] digits = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        
        [Benchmark]
        [Arguments(0)]
        [Arguments(1)]
        [Arguments(2)]
        [Arguments(3)]
        [Arguments(4)]
        [Arguments(5)]
        [Arguments(6)]
        [Arguments(7)]
        [Arguments(8)]
        [Arguments(9)]
        public int CalculateMod11(int digit)
        {
            return Mod11(digit);
        }
        
        [Benchmark]
        [Arguments(0)]
        [Arguments(1)]
        [Arguments(2)]
        [Arguments(3)]
        [Arguments(4)]
        [Arguments(5)]
        [Arguments(6)]
        [Arguments(7)]
        [Arguments(8)]
        [Arguments(9)]
        public int CalculateMod11_2(int digit)
        {
            return Mod11_2(digit);
        }
        
        [Benchmark]
        [Arguments(0)]
        [Arguments(1)]
        [Arguments(2)]
        [Arguments(3)]
        [Arguments(4)]
        [Arguments(5)]
        [Arguments(6)]
        [Arguments(7)]
        [Arguments(8)]
        [Arguments(9)]
        public int CalculateMod11_21(int digit)
        {
            return Mod11_21(digit);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11(int value)
        {
            var result = 11 - value % 11;
            return result < 10 ? result : 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11_2(int value)
        {
            var result = value % 11;
            return result == 10 ? 0 : result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Mod11_21(int value) => (value % 11) % 10;
    }
}