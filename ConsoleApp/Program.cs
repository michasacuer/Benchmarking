using BenchmarkDotNet.Running;
using Benchmarking;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LinqDoubleLoop>();
        }
    }
}
