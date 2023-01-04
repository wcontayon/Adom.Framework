#pragma warning disable CA1852

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Adom.Framework.Security.Perf
{
    class Program
    {
        static void Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Hello, World!");
            BenchmarkRunner.Run<BCryptHelperBenchmark>();

            Console.ReadLine();
        }
    }

    [MemoryDiagnoser]
    public class BCryptHelperBenchmark
    {
        [Params(1, 2, 3, 4, 5, 6, 7)]
        public int salt { get; set; }

        string unhashedText = "Phrase to hase - wazertyuip1234567890@ç_è-(('";

        [Benchmark]
        public void HashSaltOne() => BCryptHelper.HashPassword(unhashedText, BCryptHelper.GenerateSalt(salt));
    }
}

