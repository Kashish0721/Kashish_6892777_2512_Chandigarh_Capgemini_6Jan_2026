using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        int root = (int)Math.Sqrt(num);

        int lower = root * root;
        int upper = (root + 1) * (root + 1);

        int closest = (num - lower < upper - num) ? lower : upper;

        Console.WriteLine("Closest perfect square: " + closest);
    }
}
