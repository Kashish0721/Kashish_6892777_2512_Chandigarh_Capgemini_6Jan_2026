using System;
using System.Linq;

class Program
{
    static bool AreAllAnagrams(string[] words)
    {
        string reference = String.Concat(words[0].OrderBy(c => c));

        foreach (string word in words)
        {
            string sorted = String.Concat(word.OrderBy(c => c));

            if (!sorted.Equals(reference))
                return false;
        }

        return true;
    }

    static void Main()
    {
        Console.Write("Enter comma separated words: ");
        string input = Console.ReadLine();

        string[] words = input.Split(',');

        Console.WriteLine(AreAllAnagrams(words));
    }
}
