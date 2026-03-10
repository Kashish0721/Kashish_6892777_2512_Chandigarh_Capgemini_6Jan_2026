using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int convertRomanToDecimal(string s)
    {
        if (string.IsNullOrEmpty(s))
            return -1;

        Dictionary<char, int> map = new Dictionary<char, int>()
        {
            {'I',1}, {'V',5}, {'X',10}, {'L',50},
            {'C',100}, {'D',500}, {'M',1000}
        };

        int total = 0;

        for (int i = 0; i < s.Length; i++)
        {
            // Business Rule: invalid character
            if (!map.ContainsKey(s[i]))
                return -1;

            int value = map[s[i]];

            if (i + 1 < s.Length && map[s[i + 1]] > value)
            {
                total -= value; // subtractive notation
            }
            else
            {
                total += value;
            }
        }

        return total;
    }
}
