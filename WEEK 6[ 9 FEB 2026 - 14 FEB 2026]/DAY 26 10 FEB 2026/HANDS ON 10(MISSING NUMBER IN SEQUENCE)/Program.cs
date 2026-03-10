using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter numbers: ");
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        int n = arr.Length + 1;

        int expected = n * (n + 1) / 2;
        int actual = arr.Sum();

        Console.WriteLine("Missing number: " + (expected - actual));
    }
}
