using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarking
{
    public class LinqSelectForeach
    {
        int dataCount = 1000;

        [Benchmark]
        public List<ClassB> LinqSelect()
        {
            var listA = InitializeClassA(dataCount);
            var listB = listA.Select(a => new ClassB 
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
            var listA = InitializeClassA(dataCount);

            var listB = new List<ClassB>();
            listA.ForEach(a => listB.Add(new ClassB
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
            var listA = InitializeClassA(dataCount);

            var listB = new List<ClassB>();
            foreach (var item in listA)
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
