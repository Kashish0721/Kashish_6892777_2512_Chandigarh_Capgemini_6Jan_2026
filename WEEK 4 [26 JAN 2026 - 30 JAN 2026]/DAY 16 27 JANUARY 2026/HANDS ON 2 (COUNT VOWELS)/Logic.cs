using System;

class Logic
{
    public int CountVowels(string s)
    {
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            char c = char.ToLower(s[i]);
            if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                count++;
        }
        return count;
    }
}
