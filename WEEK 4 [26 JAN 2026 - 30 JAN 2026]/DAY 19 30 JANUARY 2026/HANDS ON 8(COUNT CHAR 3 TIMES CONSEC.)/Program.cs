using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string:");
        string s = Console.ReadLine();

        int count = Logic.CountTripleRepeat(s);
        Console.WriteLine("Output: " + count);
    }
}
