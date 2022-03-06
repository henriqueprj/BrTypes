using BenchmarkDotNet.Attributes;

namespace BrTypes.Tests.Benchmarks.Cpf
{
    [MemoryDiagnoser]
    public class Cpf2ToStringBenchmark
    {
        private readonly Cpf2 _value;
        
        public Cpf2ToStringBenchmark()
        {
            _value = Cpf2.Parse("12345678909");
        }

        [Benchmark]
        public string Cpf2_ToString()
        {
            return _value.ToString();
        }
    }
}