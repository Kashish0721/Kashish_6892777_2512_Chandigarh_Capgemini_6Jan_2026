using System;

class Program
{
    static bool IsPalindrome(string s)
    {
        int start = 0, end = s.Length - 1;

        while (start < end)
        {
            if (s[start] != s[end])
                return false;

            start++;
            end--;
        }
        return true;
    }

    static void Main()
    {
        Console.Write("Enter string: ");
        string str = Console.ReadLine();

        int score = 0;

        for (int i = 0; i <= str.Length - 4; i++)
        {
            if (IsPalindrome(str.Substring(i, 4)))
                score += 5;
        }

        for (int i = 0; i <= str.Length - 5; i++)
        {
            if (IsPalindrome(str.Substring(i, 5)))
                score += 10;
        }

        Console.WriteLine("Score: " + score);
    }
}
