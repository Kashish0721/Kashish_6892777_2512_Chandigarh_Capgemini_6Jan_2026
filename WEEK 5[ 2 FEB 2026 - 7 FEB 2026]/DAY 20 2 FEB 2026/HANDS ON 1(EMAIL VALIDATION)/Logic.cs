using System;

class Logic
{
    public static int ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || email.Length > 100)
            return -1;

        int at = email.IndexOf('@');
        int dot = email.LastIndexOf('.');

        if (at <= 0) return -1;                 // @ not at start
        if (email.IndexOf('@', at + 1) != -1)  // only one @
            return -1;
        if (dot < at + 2) return -1;            // . after @
        if (dot == email.Length - 1) return -1; // . not at end
        if (email.Contains(" ")) return -1;     // no spaces

        return 1;
    }
}
