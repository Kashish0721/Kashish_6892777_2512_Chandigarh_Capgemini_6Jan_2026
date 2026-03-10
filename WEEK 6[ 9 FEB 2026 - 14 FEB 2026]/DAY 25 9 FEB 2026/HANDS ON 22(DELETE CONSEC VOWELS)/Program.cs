using System;

class Program
{
    static bool IsVowel(char c)
    {
        return "aeiouAEIOU".Contains(c);
    }

    static void Main()
    {
        string s = Console.ReadLine();
        int deletions = 0;

        for (int i = 0; i < s.Length - 1; i++)
        {
            if (IsVowel(s[i]) && IsVowel(s[i + 1]))
                deletions++;
        }

        Console.WriteLine("Deletions: " + deletions);
    }
}
