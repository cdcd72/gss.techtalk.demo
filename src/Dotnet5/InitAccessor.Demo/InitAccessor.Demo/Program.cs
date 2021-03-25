using InitAccessor.Demo.Model;
using System;

namespace InitAccessor.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Person("Neil", "Tsai");
            var p2 = new Person("Neil", "Tsai");
            var s1 = new Student("Neil", "Tsai", 87);

            Console.WriteLine($"p1.ToString(): {p1}");
            Console.WriteLine($"p2.ToString(): {p2}");

            Console.WriteLine();

            Console.WriteLine($"ReferenceEquals(p1, p2): {ReferenceEquals(p1, p2)}");
            Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");
            Console.WriteLine($"Equals(p1, p2): {Equals(p1, p2)}");
            Console.WriteLine($"p1 == p2: {p1 == p2}");

            Console.WriteLine("=========================================================================");

            Console.WriteLine($"p1.ToString(): {p1}");
            Console.WriteLine($"s1.ToString(): {s1}");

            Console.WriteLine();

            Console.WriteLine($"ReferenceEquals(p1, s1): {ReferenceEquals(p1, s1)}");
            Console.WriteLine($"p1.Equals(s1): {p1.Equals(s1)}");
            Console.WriteLine($"Equals(p1, s1): {Equals(p1, s1)}");
            Console.WriteLine($"p1 == s1: {p1 == s1}");

            // 沒辦法修改 p1，因為 init 存取子關係...
            //p1.FirstName = "Pekora";
        }
    }
}
