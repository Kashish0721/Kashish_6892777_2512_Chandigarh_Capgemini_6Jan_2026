using System;
using System.Text;

class Logic
{
    public string ProcessString(string s1, string s2)
    {
        StringBuilder temp = new StringBuilder();

        // Step 1: remove common consonants
        foreach (char c in s1)
        {
            if (IsConsonant(c) && ContainsIgnoreCase(s2, c))
                continue;

            temp.Append(c);
        }

        // Step 2: remove consecutive duplicates
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < temp.Length; i++)
        {
            if (i == 0 || char.ToLower(temp[i]) != char.ToLower(temp[i - 1]))
                result.Append(temp[i]);
        }

        return result.ToString();
    }

    bool IsConsonant(char c)
    {
        char x = char.ToLower(c);
        return (x >= 'a' && x <= 'z') && !"aeiou".Contains(x);
    }

    bool ContainsIgnoreCase(string s, char c)
    {
        foreach (char x in s)
            if (char.ToLower(x) == char.ToLower(c))
                return true;
        return false;
    }
}
