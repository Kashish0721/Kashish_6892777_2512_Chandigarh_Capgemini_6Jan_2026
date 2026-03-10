using System;

namespace Q4_SOLVE_THE_EQUATION_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();

            int a = int.Parse(input[0]);
            int b = int.Parse(input[1]);
            int c = int.Parse(input[2]);   // agar required ho

            int result = (a * a * a) + (3 * a * a * b) + (3 * a * b * b) + (b * b * b);

            Console.WriteLine(result);
        }
    }
}
