using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BrTypes.Tests.Benchmarks.Cnpj
{
    [RPlotExporter]
    [MemoryDiagnoser]
    public class CnpjBenchmark
    {
        [Params("00122258000160", "00.122.258/0001-60")]
        public string? Value { get; set; }
        
        [Benchmark(Baseline=true)]
        public (bool, BrTypes.Cnpj) TryParse()
        {
            var isValid = BrTypes.Cnpj.TryParse(Value, out var cnpj);
            return (isValid, cnpj);
        }
        
        [Benchmark]
        public (bool, BrTypes.Cnpj) TryParse2()
        {
            var isValid = BrTypes.Cnpj.TryParse2(Value, out var cnpj);
            return (isValid, cnpj);
        }
        
        [Benchmark]
        public (bool, BrTypes.Cnpj) TryParse3()
        {
            var isValid = BrTypes.Cnpj.TryParse3(Value, out var cnpj);
            return (isValid, cnpj);
        }
        
        [Benchmark]
        public (bool, BrTypes.Cnpj) TryParse4()
        {
            var isValid = BrTypes.Cnpj.TryParse4(Value, out var cnpj);
            return (isValid, cnpj);
        }
        
        [Benchmark]
        public (bool, BrTypes.Cnpj) TryParse5()
        {
            var isValid = BrTypes.Cnpj.TryParse5(Value, out var cnpj);
            return (isValid, cnpj);
        }
    }
}