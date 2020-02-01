using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarking
{

// data count = 10000
// 
// |        Method |     Mean |   Error |  StdDev |    Gen 0 | Gen 1 | Gen 2 | Allocated |
// |-------------- |---------:|--------:|--------:|---------:|------:|------:|----------:|
// |    BiggerLinq | 158.8 ms | 3.12 ms | 3.94 ms | 250.0000 |     - |     - |   1.22 MB |
// | BiggerForeach | 153.2 ms | 3.05 ms | 6.10 ms | 250.0000 |     - |     - |   1.22 MB |

    [MemoryDiagnoser]
    public class LinqFirstListCount
    {
        int dataCount = 10000;

        public LinqFirstListCount()
        {
            A1 = InitializeClassA(dataCount);
            B1 = InitializeClassB(dataCount);
            A2 = InitializeClassA(dataCount);
            B2 = InitializeClassB(dataCount);
        }

        public List<ClassA> A1 { get; set; }
        public List<ClassB> B1 { get; set; }
        public List<ClassA> A2 { get; set; }
        public List<ClassB> B2 { get; set; }

        [Benchmark]
        public List<ClassA> BiggerLinq()
        {
            // bigger collection in linq
            foreach (var b in B1)
            {
                var a = A1.FirstOrDefault(x => x.P3.Equals(b.P3));
                if (b != null)
                    a.P4 = b.P3;
            }

            return A2;
        }

        [Benchmark]
        public List<ClassA> BiggerForeach()
        {
            // bigger collection in foreach
            foreach (var a in A2)
            {
                var b = B2.FirstOrDefault(x => x.P3.Equals(a.P3));
                if (b != null)
                    a.P4 = b.P3;
            }

            return A2;
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
