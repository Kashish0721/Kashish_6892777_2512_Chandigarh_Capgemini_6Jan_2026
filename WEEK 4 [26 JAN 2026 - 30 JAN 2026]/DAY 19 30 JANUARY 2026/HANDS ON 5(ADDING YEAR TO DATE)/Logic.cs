using System;

class Logic
{
    public static string AddYearsToDate(string date, int years)
    {
        // Business Rule 2
        if (years < 0)
            return "-2";

        // Business Rule 1
        if (!DateTime.TryParse(date, out DateTime d))
            return "-1";

        DateTime newDate = d.AddYears(years);
        return newDate.ToString("dd/MM/yyyy");
    }
}
