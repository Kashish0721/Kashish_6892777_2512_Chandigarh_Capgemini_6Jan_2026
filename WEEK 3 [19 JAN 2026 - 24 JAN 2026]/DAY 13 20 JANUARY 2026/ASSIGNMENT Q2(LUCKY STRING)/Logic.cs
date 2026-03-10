using System;

class Logic
{
    public string IsLuckyString(int n, string s)
    {
        if (n > s.Length)
            return "Invalid";

        for (int i = 0; i <= s.Length - n; i++)
        {
            string sub = s.Substring(i, n);

            // Check only P,S,G present
            foreach (char c in sub)
                if (c != 'P' && c != 'S' && c != 'G')
                    goto Next;

            int need = n / 2;

            // check consecutive P or S or G
            if (HasConsecutive(sub, 'P', need) ||
                HasConsecutive(sub, 'S', need) ||
                HasConsecutive(sub, 'G', need))
                return "Yes";

            Next:;
        }

        return "No";
    }

    bool HasConsecutive(string s, char ch, int need)
    {
        int cnt = 0;
        foreach (char c in s)
        {
            if (c == ch)
            {
                cnt++;
                if (cnt >= need) return true;
            }
            else cnt = 0;
        }
        return false;
    }
}
