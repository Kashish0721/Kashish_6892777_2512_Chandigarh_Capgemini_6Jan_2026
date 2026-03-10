using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a sentence:");
        string input = Console.ReadLine();

        string result = UserProgramCode.negativeString(input);
        Console.WriteLine("Output:");
        Console.WriteLine(result);
    }
}
