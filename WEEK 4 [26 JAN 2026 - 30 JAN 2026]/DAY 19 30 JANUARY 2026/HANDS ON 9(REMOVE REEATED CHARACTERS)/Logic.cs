using System;
using System.Collections.Generic;

class Logic
{
    public static string RemoveDuplicates(string s)
    {
        HashSet<char> seen = new HashSet<char>();
        string result = "";

        foreach (char c in s)
        {
            if (!seen.Contains(c))
            {
                result += c;
                seen.Add(c);
            }
        }
        return result;
    }
}
