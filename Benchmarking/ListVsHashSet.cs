using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    public class Class1
    {
        public ICollection<string> HashSet { get; set; } = new HashSet<string>(new[] {"a", "b", "c", "d", "e"});
    }
    
    public class Class2
    {
        public ICollection<string> List { get; set; } = new List<string>(new[] {"a", "b", "c", "d", "e"});
    }
    
    [MemoryDiagnoser]
    public class ListVsHashSet
    {
        [Benchmark]
        public void HashSet()
        {
            var list1 = new List<Class1>();
            
            for (int i = 0; i < 100000; i++)
            {
                list1.Add(new Class1());
            }
        }
        
        [Benchmark]
        public void List()
        {
            var list2 = new List<Class2>();

            for (int i = 0; i < 100000; i++)
            {
                list2.Add(new Class2());
            }
        }
    }
}