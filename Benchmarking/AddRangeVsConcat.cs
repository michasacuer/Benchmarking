using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class AddRangeVsConcat
    {
        int dataCount = 100000;

        public List<string> ListA { get; set; }
        public List<string> ListB { get; set; }
        public List<string> ListC { get; set; }
        public List<string> ListD { get; set; }

        public AddRangeVsConcat()
        {
            ListA = InitializeList(dataCount);
            ListB = InitializeList(dataCount);
            ListC = InitializeList(dataCount);
            ListD = InitializeList(dataCount);
        }

        [Benchmark]
        public List<string> ConcatTest()
        {
            return new[] {"dumpString"}
                .Concat(ListA)
                .Concat(ListB)
                .Concat(ListC)
                .Concat(ListD)
                .ToList();
        }

        [Benchmark]
        public List<string> AddRangeTest()
        {
            var list = new List<string>() { "dumpString" };
            list.AddRange(ListA);
            list.AddRange(ListB);
            list.AddRange(ListC);
            list.AddRange(ListD);

            return list;
        }
        
        public List<string> InitializeList(int dataCount)
        {
            var result = new List<string>();
            for (int i = 0; i < dataCount; i++)
            {
                Random random = new Random();
                string str = random.Next(0, 1000).ToString();
                result.Add(str);
            }

            return result;
        }
    }
}