using System;
using BenchmarkDotNet.Attributes;

namespace BrTypes.Tests.Benchmarks.Cpf
{
    [MemoryDiagnoser]
    public class CpfVsCpf2Benchmark
    {
        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse()
        {
            BrTypes.Cpf2.TryParse("12345678909", out var cpf);
            return cpf;
        }
        
        [Benchmark(Baseline=true)]
        public BrTypes.Cpf Cpf_TryParse()
        {
            BrTypes.Cpf.TryParse("12345678909", out var cpf);
            return cpf;
        }

        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse0()
        {
            BrTypes.Cpf2.TryParse0("12345678909", out var cpf);
            return cpf;
        }

        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse2()
        {
            BrTypes.Cpf2.TryParse2("12345678909", out var cpf);
            return cpf;
        }
        
        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse3()
        {
            BrTypes.Cpf2.TryParse3("12345678909", out var cpf);
            return cpf;
        }
        
        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse4()
        {
            BrTypes.Cpf2.TryParse4("12345678909", out var cpf);
            return cpf;
        }

        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse55()
        {
            BrTypes.Cpf2.TryParse55("12345678909", out var cpf);
            return cpf;
        }
        
        [Benchmark]
        public BrTypes.Cpf2 Cpf2_TryParse6()
        {
            BrTypes.Cpf2.TryParse6("12345678909", out var cpf);
            return cpf;
        }
    }
}