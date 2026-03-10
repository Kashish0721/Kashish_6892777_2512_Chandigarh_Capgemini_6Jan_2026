using System;
using System.Globalization;

class Logic
{
    public static string AddYearFindDay(string date)
    {
        if (!DateTime.TryParseExact(
            date, "dd/MM/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateTime d))
            return "-1";

        return d.AddYears(1).DayOfWeek.ToString();
    }
}
