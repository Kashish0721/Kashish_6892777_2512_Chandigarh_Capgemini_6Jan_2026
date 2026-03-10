using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of friends: ");
        int N = int.Parse(Console.ReadLine());

        Console.Write("Enter seconds: ");
        int T = int.Parse(Console.ReadLine());

        int from = (T - 1) % N;
        int to = T % N;

        Console.WriteLine($"Friend {from + 1} passed the ball to Friend {to + 1}");
    }
}
