using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int largestNumber(int[] input1)
    {
        bool hasNeg = false, hasZeroOrGt100 = false;

        // Remove duplicates
        HashSet<int> set = new HashSet<int>();

        foreach (int x in input1)
        {
            if (x < 0) hasNeg = true;
            if (x == 0 || x > 100) hasZeroOrGt100 = true;
            set.Add(x);
        }

        if (hasNeg && hasZeroOrGt100) return -3;
        if (hasNeg) return -1;
        if (hasZeroOrGt100) return -2;

        int sum = 0;

        // Ranges: 1-10, 11-20, ... , 91-100
        for (int start = 1; start <= 91; start += 10)
        {
            int end = start + 9;
            int max = -1;

            foreach (int x in set)
            {
                if (x >= start && x <= end && x > max)
                    max = x;
            }

            if (max != -1)
                sum += max;
        }

        return sum;
    }
}
