using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter binary string: ");
        string s = Console.ReadLine();

        int zeros = 0;
        int maxLen = 0;

        foreach (char c in s)
        {
            if (c == '0')
                zeros++;

            maxLen = Math.Max(maxLen, zeros);
        }

        int ones = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == '1')
                ones++;

            maxLen = Math.Max(maxLen, ones);
        }

        Console.WriteLine("Longest possible length: " + (zeros + ones));
    }
}
