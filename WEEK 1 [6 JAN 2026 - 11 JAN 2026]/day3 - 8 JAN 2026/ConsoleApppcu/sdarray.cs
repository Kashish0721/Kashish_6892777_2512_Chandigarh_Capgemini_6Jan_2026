

//using System;

//class sdarray
//{
//    static void Main(string[] args)
//    {
//        int[] arr = new int[5];

//        Console.WriteLine("Enter 5 numbers:");

//        for (int i = 0; i < 5; i++)
//        {
//            arr[i] = int.Parse(Console.ReadLine());
//        }

//        Console.WriteLine("\nArray elements are:");

//        for (int i = 0; i < 5; i++)
//        {
//            Console.WriteLine(arr[i]);
//        }

//        Console.ReadLine();
//    }
//}

//class sdarray
//{
//    static void Main({
//        int i;
//        int[] arr
//    })
//}


using System;

namespace ConsoleApppcu   // collection of classes
{
    internal class PassingParameters
    {
        void test1()
        {
            Console.WriteLine("This is first method.");
        }

        void test2(int x)
        {
            Console.WriteLine("This is 2nd method.");
        }

        string test3()
        {
            return "This is 3rd method";
        }

        string test4(string name)
        {
            return "Hello" + name;
        }

        void math1(int x, int y, ref int a, ref int b)
        {
            a = x + y;
            b = x * y;
        }

        void math2(int x, int y, out int a, out int b)
        {
            a = x - y;
            b = x / y;
        }


        static void Main(string[] args)
        {
            PassingParameters p = new PassingParameters();
            p.test1();p.test2(100);
            Console.WriteLine(p.test3());
            Console.WriteLine(p.test4("tej"));
            int m = 0, n = 0;
            p.math1(100, 50, ref m, ref n);
            Console.WriteLine(m + " " + n);
            int q, r;
            p.math2(100, 50, out q, out r);
            Console.WriteLine(q + " " + r);
            Console.ReadLine();
        }
    }
} 