using System;

class UserProgramCode
{
    public static string formString(string[] input1, int input2)
    {
        // Business Rule: special characters not allowed in input strings
        foreach (string s in input1)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c))
                    return "-1";
            }
        }

        string result = "";

        // input2 is nth character (1-based index)
        int idx = input2 - 1;

        foreach (string s in input1)
        {
            if (idx >= 0 && idx < s.Length)
                result += s[idx];
            else
                result += '$';
        }

        return result;
    }
}
