using System;

class UserProgramCode
{
    public static int validateTime(string str)
    {
        if (str == null || str.Length != 8)
            return -1;

        // Must be hh:mm am/pm
        if (str[2] != ':' || str[5] != ' ')
            return -1;

        if (!int.TryParse(str.Substring(0, 2), out int hr)) return -1;
        if (!int.TryParse(str.Substring(3, 2), out int min)) return -1;

        string suf = str.Substring(6, 2).ToLower();

        if (hr < 1 || hr > 12) return -1;
        if (min < 0 || min > 59) return -1;
        if (suf != "am" && suf != "pm") return -1;

        return 1;
    }
}
