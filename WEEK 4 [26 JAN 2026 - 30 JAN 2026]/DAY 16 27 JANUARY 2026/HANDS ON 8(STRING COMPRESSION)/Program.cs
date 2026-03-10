using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string to compress:");
        string s = Console.ReadLine();

        Logic obj = new Logic();
        string result = obj.CompressString(s);

        Console.WriteLine("Output: " + result);
    }
}
