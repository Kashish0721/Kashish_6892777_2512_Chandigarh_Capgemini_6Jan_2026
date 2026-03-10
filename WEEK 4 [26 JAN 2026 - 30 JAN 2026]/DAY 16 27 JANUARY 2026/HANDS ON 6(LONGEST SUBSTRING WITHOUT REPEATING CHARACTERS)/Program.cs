using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        Logic obj = new Logic();
        int output = obj.LengthOfLongestSubstring(input);

        Console.WriteLine("Output: " + output);
    }
}
