// See https://aka.ms/new-console-template for more information
using System;

namespace Adom.Framework.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var lines = StringExtensions.SplitedSample1();
            Console.WriteLine(string.Join(" | ", lines));

            var lines2 = StringExtensions.SplitedSample2();
            Console.WriteLine(string.Join(" | ", lines2));

            Console.WriteLine($"FakeGuid {StringExtensions.IsGuidFalse()}");

            Console.WriteLine($"True Guid {StringExtensions.IsGuidTrue()}");
        }
    }
}
