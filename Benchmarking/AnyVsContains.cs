using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class AnyVsContains
    {
        int dataCount = 100000;

        public List<ClassA> anyA { get; set; }
        public List<ClassB> anyB { get; set; }
        public List<ClassA> containsA { get; set; }
        public List<string> containsB { get; set; }

        public AnyVsContains()
        {
            anyA = InitializeClassA(dataCount);
            anyB = InitializeClassB(dataCount);
            containsA = InitializeClassA(dataCount);
            containsB = InitializeIdClass(dataCount);
        }

        [Benchmark]
        public List<ClassA> AnyTest()
        {
            var list = anyA.Where(a => anyB.Any(b => b.P1 == a.P1)).ToList();
            return list;
        }

        [Benchmark]
        public List<ClassA> ContainsTest()
        {
            var list = containsA.Where(x => containsB.Contains(x.P1)).ToList();
            return list;
        }

        public class ClassA
        {
            public string P1 { get; set; }
            public string P2 { get; set; }
            public string P3 { get; set; }
            public string P4 { get; set; }
        }

        public class ClassB
        {
            public string P1 { get; set; }
            public string P2 { get; set; }
            public string P3 { get; set; }
        }

        public List<ClassA> InitializeClassA(int dataCount)
        {
            var result = new List<ClassA>();
            for (int i = 0; i < dataCount; i++)
            {
                Random random = new Random();
                string field1 = random.Next(0, 1000).ToString();
                string field2 = random.Next(0, 1000).ToString();
                string field3 = random.Next(0, 1000).ToString();

                result.Add(new ClassA
                {
                    P1 = field1,
                    P2 = field2,
                    P3 = field3,
                    P4 = field3
                });
            }

            return result;
        }

        public List<ClassB> InitializeClassB(int dataCount)
        {
            int count = dataCount / 100;
            var result = new List<ClassB>();
            for (int i = 0; i < dataCount; i++)
            {
                Random random = new Random();
                string field1 = random.Next(0, 1000).ToString();
                string field2 = random.Next(0, 1000).ToString();
                string field3 = random.Next(0, 1000).ToString();

                result.Add(new ClassB
                {
                    P1 = field1,
                    P2 = field2,
                    P3 = field3
                });
            }

            return result;
        }

        public List<string> InitializeIdClass(int dataCount)
        {
            int count = dataCount / 100;
            var result = new List<string>();
            for (int i = 0; i < dataCount; i++)
            {
                Random random = new Random();
                string field1 = random.Next(0, 1000).ToString();

                result.Add(field1);
            }

            return result;
        }
    }
}