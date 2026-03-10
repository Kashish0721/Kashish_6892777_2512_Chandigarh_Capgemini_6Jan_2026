using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string:");
        string input = Console.ReadLine();

        string output = Logic.RemoveDuplicates(input);
        Console.WriteLine("Output: " + output);
    }
}
