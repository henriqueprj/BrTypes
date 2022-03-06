using System;
using System.Diagnostics;
using BenchmarkDotNet.Running;
using BrTypes;
using BrTypes.Tests.Benchmarks.Cpf;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher
                .FromAssembly(typeof(Cpf2ToStringBenchmark).Assembly).Run(args);
        
            //RunBenchmarks(); 
            // RunSimpleTests();
        }

        private static void RunBenchmarks()
        {
            var summary = BenchmarkRunner.Run<BrTypes.Tests.Benchmarks.Cpf.CpfVsCpf2Benchmark>();
            //var summary = BenchmarkRunner.Run<BrTypes.Tests.Benchmarks.Cnpj.CnpjBenchmark>();
            //var summary = BenchmarkRunner.Run<BrTypes.Tests.Benchmarks.Digits.DigitsBenchmark>();
            //var summary = BenchmarkRunner.Run<BrTypes.Tests.Benchmarks.Cpf.Cpf2ToStringBenchmark>();
            // var summary = BenchmarkRunner.Run<BrTypes.Tests.Benchmarks.Digits.CalculateDVBenchmark>();
        }

        private static void RunSimpleTests()
        {
            Span<int> digits = stackalloc int[2];
            //var b1 = BrTypes.Digits.Fill(digits, "07.245.465/0001-51", 14);
            var b2 = BrTypes.DigitZ.Fill2(digits, "1.2");
            //Console.WriteLine(b1);
            Console.WriteLine(b2);
            
            // var sw = new Stopwatch();
            // var before2 = GC.CollectionCount(2);
            // var before1 = GC.CollectionCount(1);
            // var before0 = GC.CollectionCount(0);
            //
            // sw.Start();
            // for (int i = 0; i < 1_000_000; i++)
            // {
            //     if (!Cpf.TryParse("61709754079", out _))
            //         throw new ApplicationException("Error");
            //     if (Cpf.TryParse("61709754070", out _))
            //         throw new ApplicationException("Error");
            // }
            // sw.Stop();
            //
            // Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            // Console.WriteLine($"GC Gen #2 : {GC.CollectionCount(2) - before2}");
            // Console.WriteLine($"GC Gen #1 : {GC.CollectionCount(1) - before1}");
            // Console.WriteLine($"GC Gen #0 : {GC.CollectionCount(0) - before0}");
            // Console.WriteLine("Done!");
        }
    }
}