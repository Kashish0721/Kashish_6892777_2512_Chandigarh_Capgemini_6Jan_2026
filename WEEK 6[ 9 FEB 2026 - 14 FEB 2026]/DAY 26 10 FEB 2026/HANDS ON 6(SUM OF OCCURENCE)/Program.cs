using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter first list: ");
        int[] first = Array.ConvertAll(Console.ReadLine().Split(','), int.Parse);

        Console.Write("Enter second list: ");
        int[] second = Array.ConvertAll(Console.ReadLine().Split(','), int.Parse);

        foreach (int n in first)
        {
            int sum = 0;

            foreach (int m in second)
            {
                if (m == n)
                    sum += m;
            }

            Console.WriteLine($"{n}-{sum}");
        }
    }
}
