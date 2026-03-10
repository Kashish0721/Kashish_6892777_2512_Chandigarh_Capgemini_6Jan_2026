using System;
using System.Linq;

class Program
{
    static bool IsValid(string word)
    {
        if (word.Length <= 2)
            return false;

        if (!word.All(char.IsLetterOrDigit))
            return false;

        string vowels = "aeiouAEIOU";

        bool hasVowel = word.Any(c => vowels.Contains(c));
        bool hasConsonant = word.Any(c => char.IsLetter(c) && !vowels.Contains(c));

        return hasVowel && hasConsonant;
    }

    static void Main()
    {
        Console.Write("Enter sentence: ");
        string input = Console.ReadLine();

        string[] words = input.Split(' ');

        int count = words.Count(IsValid);

        Console.WriteLine("Valid words: " + count);
    }
}
