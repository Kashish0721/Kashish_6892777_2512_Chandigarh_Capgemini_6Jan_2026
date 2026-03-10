using System;

class Program
{
    static int MaxDeletions(string s)
    {
        int count = 0;

        for (int i = 0; i < s.Length - 1; i++)
        {
            if (s[i] == s[i + 1])
            {
                count++;
                i++; // skip next character
            }
        }

        return count;
    }

    static void Main()
    {
        Console.Write("Enter string: ");
        string s = Console.ReadLine();

        Console.WriteLine("Maximum deletions: " + MaxDeletions(s));
    }
}
