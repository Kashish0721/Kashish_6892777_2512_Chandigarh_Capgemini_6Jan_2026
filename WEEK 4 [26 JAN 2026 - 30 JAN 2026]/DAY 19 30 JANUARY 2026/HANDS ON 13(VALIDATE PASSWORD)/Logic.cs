using System;

class Logic
{
    public static int ValidatePassword(string s)
    {
        if (s.Length < 8) return -1;
        if (char.IsDigit(s[0]) || !char.IsLetterOrDigit(s[0])) return -1;
        if (!char.IsLetterOrDigit(s[s.Length - 1])) return -1;

        bool hasLetter = false, hasDigit = false, hasSpecial = false;

        foreach (char c in s)
        {
            if (char.IsLetter(c)) hasLetter = true;
            else if (char.IsDigit(c)) hasDigit = true;
            else if (c == '@' || c == '#' || c == '_') hasSpecial = true;
        }

        return (hasLetter && hasDigit && hasSpecial) ? 1 : -1;
    }
}
