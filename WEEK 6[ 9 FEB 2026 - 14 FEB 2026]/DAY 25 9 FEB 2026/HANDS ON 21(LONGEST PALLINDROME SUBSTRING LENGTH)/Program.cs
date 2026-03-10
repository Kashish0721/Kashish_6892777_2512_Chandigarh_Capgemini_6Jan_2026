using System;

class Program
{
    static int Expand(string s, int left, int right)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }
        return right - left - 1;
    }

    static void Main()
    {
        string s = Console.ReadLine();
        int max = 0;

        for (int i = 0; i < s.Length; i++)
        {
            int len1 = Expand(s, i, i);
            int len2 = Expand(s, i, i + 1);

            max = Math.Max(max, Math.Max(len1, len2));
        }

        Console.WriteLine("Longest length: " + max);
    }
}
