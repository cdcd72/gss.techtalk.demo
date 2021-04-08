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

            // ==============================================================================================

            // --- C# 9 "之前"不變性的可能做法 ---

            // 使用步驟 1 來初始化
            // var x = new X() { Y = "Yee" }; // 可以使用 Object Initializer 來初始化物件，但直接失去不變性(很難預防)...
            // Console.WriteLine();           // Yee
            // x.Y = "Haha";
            // Console.WriteLine();           // Haha

            // 使用步驟 3 來初始化
            // var x = new X() { Y = "Yee" }; // 沒辦法使用 Object Initializer 來初始化物件...
            // var x = new X("Yee");          // 只能用 Constructor 來初始化物件...
            // x.Y = "Haha";                  // CS0200 error

            // --- C# 9 "之後"不變性的可能做法 ---

            // 使用步驟 4 來初始化
            // var x = new X() { Y = null };  // 可以使用 Object Initializer 來初始化物件...
            // x.Y = "Haha";                  // CS8852 error

            // 使用步驟 5 來初始化
            var x = new X() { Y = null };     // 可以使用 Object Initializer 來初始化物件...
            //x.Y = "Haha";                   // CS8852 error
        }

        /// <summary>
        /// 步驟 1
        /// </summary>
        //public class X
        //{
        //    public string Y { get; set; }
        //}

        /// <summary>
        /// 步驟 2
        /// </summary>
        //public class X
        //{
        //    public string Y { get; }
        //}

        /// <summary>
        /// 步驟 3
        /// </summary>
        //public class X
        //{
        //    public string Y { get; }

        //    public X(string y)
        //    {
        //        Y = y;
        //    }
        //}

        /// <summary>
        /// 步驟 4
        /// </summary>
        //public class X
        //{
        //    public string Y { get; init; }
        //}

        /// <summary>
        /// 步驟 5
        /// </summary>
        public class X
        {
            private readonly string _y;

            public string Y
            {
                get => _y;
                init => _y = value ?? throw new ArgumentNullException(nameof(Y));
            }
        }
    }
}
