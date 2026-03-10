using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string (only alphabets allowed):");
        Console.Write("Input: ");
        string input = Console.ReadLine();

        string result = UserProgramCode.nextString(input);
        Console.WriteLine("Output: " + result);
    }
}
