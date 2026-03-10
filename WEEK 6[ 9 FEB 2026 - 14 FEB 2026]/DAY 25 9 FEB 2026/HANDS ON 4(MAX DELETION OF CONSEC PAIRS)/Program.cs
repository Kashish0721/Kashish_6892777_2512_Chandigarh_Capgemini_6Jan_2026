using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter string: ");
        string s = Console.ReadLine();

        int count = 0;

        for (int i = 0; i < s.Length - 1; i++)
        {
            if (s[i] == s[i + 1])
            {
                count++;
                i++; // skip next
            }
        }

        Console.WriteLine("Maximum deletions: " + count);
    }
}
