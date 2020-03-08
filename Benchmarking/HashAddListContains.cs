using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarking
{
// |       Method |      Mean |     Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
// |------------- |----------:|----------:|---------:|-------:|------:|------:|----------:|
// | ListContains |  7.900 us | 0.3564 us | 1.045 us | 0.7324 |     - |     - |   2.25 KB |
// |   HashSetAdd | 10.011 us | 0.3835 us | 1.100 us | 1.4496 |     - |     - |   4.46 KB |
    [MemoryDiagnoser]
    public class HashAddListContains
    {
        private int dataCount = 100;

        [Benchmark]
        public List<int> ListContains()
        {
            var list = InitializeList();
            var result = new List<int>();
            foreach (int i in list)
            {
                if (!result.Contains(i))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        [Benchmark]
        public List<int> HashSetAdd()
        {
            var list = InitializeList();
            var set = new HashSet<int>();
            foreach (int i in list)
            {
                set.Add(i);
            }

            return set.ToList();
        }

        private List<int> InitializeList()
        {
            var list = new List<int>();
            var random = new Random();
            for (int i = 0; i < dataCount; i++)
            {
                list.Add(random.Next(0, dataCount));
            }

            return list;
        }
    }
}
