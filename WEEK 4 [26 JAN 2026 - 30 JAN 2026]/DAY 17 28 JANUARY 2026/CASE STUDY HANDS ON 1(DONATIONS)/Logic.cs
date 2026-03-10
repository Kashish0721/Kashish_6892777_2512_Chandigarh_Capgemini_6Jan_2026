using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int getDonation(string[] input1, int input2)
    {
        // Business Rule 1: Duplicates
        HashSet<string> set = new HashSet<string>();
        foreach (string s in input1)
        {
            if (set.Contains(s))
                return -1;
            set.Add(s);
        }

        int total = 0;
        string locCode = input2.ToString();

        foreach (string s in input1)
        {
            // Business Rule 2: Special characters (only digits allowed)
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return -2;
            }

            // Format: ABC DEF GHI
            // UserCode(3) + Location(3) + Amount(3)
            if (s.Length < 9) continue;

            string location = s.Substring(3, 3);
            string amountStr = s.Substring(6);

            if (location == locCode)
            {
                int amt = int.Parse(amountStr);
                total += amt;
            }
        }

        return total;
    }
}
