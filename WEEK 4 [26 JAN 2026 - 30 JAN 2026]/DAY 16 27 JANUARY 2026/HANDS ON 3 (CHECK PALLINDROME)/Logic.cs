using System;

class Logic
{
    public string IsPalindrome(string s)
    {
        int i = 0, j = s.Length - 1;

        while (i < j)
        {
            if (s[i] != s[j])
                return "No it is not a pallindrome";
            i++;
            j--;
        }

        return "yes it is a pallindrome";
    }
}
