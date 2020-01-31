using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarking
{
// for dataCount = 10000; 
//
// |        Method |      Mean |    Error |   StdDev |    Gen 0 | Gen 1 | Gen 2 | Allocated |
// |-------------- |----------:|---------:|---------:|---------:|------:|------:|----------:|
// | DoubleForeach |  89.47 ms | 0.810 ms | 0.677 ms |        - |     - |     - |     921 B |
// |     LinqFirst | 166.89 ms | 3.220 ms | 3.307 ms | 250.0000 |     - |     - | 1280060 B |
    [MemoryDiagnoser]
    public class LinqDoubleLoop
    {
        int dataCount = 10000;

        public LinqDoubleLoop()
        {
            foreachA = InitializeClassA(dataCount);
            foreachB = InitializeClassB(dataCount);
            linqA = InitializeClassA(dataCount);
            linqB = InitializeClassB(dataCount);
        }

        public List<ClassA> foreachA { get; set; }
        public List<ClassB> foreachB { get; set; }
        public List<ClassA> linqA { get; set; }
        public List<ClassB> linqB { get; set; }

        [Benchmark]
        public List<ClassA> DoubleForeach()
        {
            foreach (var a in foreachA)
            {
                foreach (var b in foreachB)
                {
                    if (a.P3.Equals(b.P3))
                    {
                        a.P4 = b.P3;
                        break;
                    }
                }
            }

            return foreachA;
        }

        [Benchmark]
        public List<ClassA> LinqFirst()
        {
            foreach (var a in linqA)
            {
                var b = linqB.FirstOrDefault(x => x.P3.Equals(a.P3));
                if (b != null)
                    a.P4 = b.P3;
            }

            return linqA;
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
    }
}
