using System;

class Logic
{
    public static int IsNumericArray(string[] arr)
    {
        foreach (string s in arr)
        {
            if (!double.TryParse(s, out _))
                return -1;
        }
        return 1;
    }
}
