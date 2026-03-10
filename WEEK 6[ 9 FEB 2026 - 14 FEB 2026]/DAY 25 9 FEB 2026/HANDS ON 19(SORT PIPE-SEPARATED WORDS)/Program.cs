using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        string[] words = input.Split('|');

        Array.Sort(words);

        Console.WriteLine(string.Join("|", words));
    }
}
