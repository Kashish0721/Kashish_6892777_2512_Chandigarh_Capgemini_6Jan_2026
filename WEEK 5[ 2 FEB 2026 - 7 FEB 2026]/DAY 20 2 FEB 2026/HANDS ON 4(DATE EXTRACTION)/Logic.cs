using System;

class Logic
{
    public static void ExtractDates(string text)
    {
        for (int i = 0; i <= text.Length - 10; i++)
        {
            string d = text.Substring(i, 10);

            if (d[2] == '/' && d[5] == '/' &&
                int.TryParse(d.Substring(0, 2), out _) &&
                int.TryParse(d.Substring(3, 2), out _) &&
                int.TryParse(d.Substring(6, 4), out _))
            {
                Console.WriteLine(d);
            }
        }
    }
}
