using BenchmarkDotNet.Running;
using Benchmarking;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<HashAddListContains>();
        }
    }
}
