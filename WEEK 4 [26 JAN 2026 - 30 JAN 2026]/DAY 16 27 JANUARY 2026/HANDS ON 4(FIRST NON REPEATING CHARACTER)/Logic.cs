using System;

class Logic
{
    public char FirstNonRepeating(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (s.IndexOf(s[i]) == s.LastIndexOf(s[i]))
                return s[i];
        }
        return '\0';
    }
}
