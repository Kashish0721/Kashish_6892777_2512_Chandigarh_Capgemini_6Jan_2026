using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static string SortWord(string word)
    {
        char[] ch = word.ToCharArray();
        Array.Sort(ch);
        return new string(ch);
    }

    static void Main()
    {
        Console.Write("Enter words separated by space: ");
        string[] words = Console.ReadLine().Split();

        Dictionary<string, int> map = new Dictionary<string, int>();

        foreach (string w in words)
        {
            string sorted = SortWord(w);

            if (map.ContainsKey(sorted))
                map[sorted]++;
            else
                map[sorted] = 1;
        }

        Console.WriteLine("Unique words:");

        foreach (string w in words)
        {
            if (map[SortWord(w)] == 1)
                Console.WriteLine(w);
        }
    }
}
