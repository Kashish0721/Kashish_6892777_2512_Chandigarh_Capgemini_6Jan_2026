using System;

class Logic
{
    public static bool IsStrongPassword(string s)
    {
        if (s.Length < 8) return false;

        bool upper = false, lower = false, digit = false, special = false;

        foreach (char c in s)
        {
            if (char.IsUpper(c)) upper = true;
            else if (char.IsLower(c)) lower = true;
            else if (char.IsDigit(c)) digit = true;
            else if ("@$!%*?&".Contains(c)) special = true;
        }

        return upper && lower && digit && special;
    }
}
