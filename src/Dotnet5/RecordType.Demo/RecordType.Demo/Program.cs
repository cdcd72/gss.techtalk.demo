﻿using System;

namespace RecordType.Demo
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

            Console.WriteLine("=========================================================================");

            // 若為有效的記錄類型(Record Type)，就可以使用解構方式(Deconstruct)讀取內部屬性值...
            var (firstName, lastName) = p1;

            Console.WriteLine("{0} {1}", firstName, lastName);

            Console.WriteLine("=========================================================================");

            // 沒辦法修改 p1 記錄(Record)，因為 init 存取子關係...
            //p1.FirstName = "Pekora";

            // 若為有效的記錄類型(Record Type)，就可以用 with 運算式"複製"記錄(Record)並修改內部屬性值 (很像 prototype pattern...)
            var p3 = p1 with { FirstName = "Pekora" };

            Console.WriteLine("{0} {1}", p3.FirstName, p3.LastName);
        }
    }

    //public record Person
    //{
    //    public string FirstName { get; init; }
    //    public string LastName { get; init; }

    //    public Person(string first, string last) => (FirstName, LastName) = (first, last);
    //}

    public record Person(string FirstName, string LastName);

    //public record Teacher : Person
    //{
    //    public string Subject { get; init; }

    //    public Teacher(string first, string last, string sub)
    //        : base(first, last) => Subject = sub;
    //}

    public record Teacher(string FirstName, string LastName, string Subject) : Person(FirstName, LastName);

    //public sealed record Student : Person
    //{
    //    public int Level { get; init; }

    //    public Student(string first, string last, int level) : base(first, last) => Level = level;
    //}

    public record Student(string FirstName, string LastName, int Level) : Person(FirstName, LastName);
}
