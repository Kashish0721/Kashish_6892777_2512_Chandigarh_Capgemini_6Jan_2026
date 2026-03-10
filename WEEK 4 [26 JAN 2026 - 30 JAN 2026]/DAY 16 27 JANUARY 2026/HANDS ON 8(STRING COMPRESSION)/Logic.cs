using System;
using System.Text;

class Logic
{
    public string CompressString(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        StringBuilder sb = new StringBuilder();
        int count = 1;

        for (int i = 1; i <= s.Length; i++)
        {
            if (i < s.Length && s[i] == s[i - 1])
            {
                count++;
            }
            else
            {
                sb.Append(s[i - 1]);
                sb.Append(count);
                count = 1;
            }
        }

        return sb.ToString();
    }
}
