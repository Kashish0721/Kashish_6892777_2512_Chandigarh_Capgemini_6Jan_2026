using System;

class Program
{
    static int ClosestPerfectSquare(int n)
    {
        int lower = (int)Math.Floor(Math.Sqrt(n));
        int upper = (int)Math.Ceiling(Math.Sqrt(n));

        int lowSq = lower * lower;
        int upSq = upper * upper;

        if (n - lowSq <= upSq - n)
            return lowSq;
        else
            return upSq;
    }

    static void Main()
    {
        Console.Write("Enter number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Closest perfect square: " + ClosestPerfectSquare(n));
    }
}
