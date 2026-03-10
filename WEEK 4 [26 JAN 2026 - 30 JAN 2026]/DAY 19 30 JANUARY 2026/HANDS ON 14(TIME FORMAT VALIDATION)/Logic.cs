using System;
using System.Globalization;

class Logic
{
    public static int ValidateTime(string t)
    {
        return DateTime.TryParseExact(
            t, "hh:mm tt", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out _)
            ? 1 : -1;
    }
}
