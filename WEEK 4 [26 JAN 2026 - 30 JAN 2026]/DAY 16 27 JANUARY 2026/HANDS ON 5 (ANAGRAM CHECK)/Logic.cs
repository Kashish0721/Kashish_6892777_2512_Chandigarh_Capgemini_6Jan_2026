using System;

class Logic
{
    public string IsAnagram(string a, string b)
    {
        if (a.Length != b.Length)
            return "No";

        char[] x = a.ToLower().ToCharArray();
        char[] y = b.ToLower().ToCharArray();

        Array.Sort(x);
        Array.Sort(y);

        return new string(x) == new string(y) ? "Yes" : "No";
    }
}
