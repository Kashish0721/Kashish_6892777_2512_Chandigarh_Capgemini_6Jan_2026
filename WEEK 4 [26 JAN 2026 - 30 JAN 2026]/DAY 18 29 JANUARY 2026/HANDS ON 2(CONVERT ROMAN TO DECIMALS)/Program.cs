using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Roman Number (e.g. XII, DCL):");
        string input = Console.ReadLine();

        int result = UserProgramCode.convertRomanToDecimal(input);
        Console.WriteLine("Output: " + result);
    }
}
