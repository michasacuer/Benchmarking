using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarking
{
// For dataCount = 10000.
// 
// |          Method |     Mean |    Error |   StdDev |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
// |---------------- |---------:|---------:|---------:|--------:|--------:|--------:|----------:|
// |      LinqSelect | 245.7 us |  8.30 us | 23.93 us | 76.1719 | 38.0859 |       - | 468.88 KB |
// |     LinqForeach | 895.3 us | 32.14 us | 92.23 us | 97.6563 | 48.8281 | 27.3438 | 647.01 KB |
// | StandardForeach | 821.5 us | 17.76 us | 28.68 us | 98.6328 | 45.8984 | 27.3438 | 646.93 KB |

    [MemoryDiagnoser]
    public class LinqSelectForeach
    {
        int dataCount = 10000;

        public List<ClassA> A1 { get; set; }
        public List<ClassA> A2 { get; set; }
        public List<ClassA> A3 { get; set; }

        public LinqSelectForeach()
        {
            A1 = InitializeClassA(dataCount);
            A2 = InitializeClassA(dataCount);
            A3 = InitializeClassA(dataCount);
        }

        [Benchmark]
        public List<ClassB> LinqSelect()
        {
            var listB = A1.Select(a => new ClassB 
            {
                Prop1 = a.Prop1,
                Prop2 = a.Prop2,
                Prop3 = a.Prop3
            });

            return listB.ToList();
        }

        [Benchmark]
        public List<ClassB> LinqForeach()
        {
            var listB = new List<ClassB>();
            A2.ForEach(a => listB.Add(new ClassB
            {
                Prop1 = a.Prop1,
                Prop2 = a.Prop2,
                Prop3 = a.Prop3
            }));

            return listB;
        }

        [Benchmark]
        public List<ClassB> StandardForeach()
        {
            var listB = new List<ClassB>();
            foreach (var item in A3)
                listB.Add(new ClassB
                {
                    Prop1 = item.Prop1,
                    Prop2 = item.Prop2,
                    Prop3 = item.Prop3
                });

            return listB;
        }

        public class ClassA
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
        }

        public class ClassB
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
        }

        private List<ClassA> InitializeClassA(int dataCount)
        {
            var result = new List<ClassA>();
            for (int i = 0; i < dataCount; i++)
            {
                result.Add(new ClassA
                {
                    Prop1 = Guid.NewGuid().ToString(),
                    Prop2 = Guid.NewGuid().ToString(),
                    Prop3 = Guid.NewGuid().ToString()
                });
            }

            return result;
        }
    }
}
