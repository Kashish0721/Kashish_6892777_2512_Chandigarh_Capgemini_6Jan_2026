using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string s = Console.ReadLine();

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < s.Length; i += 2)
        {
            result.Append(s[i]);
        }

        Console.WriteLine("Result: " + result);
    }
}