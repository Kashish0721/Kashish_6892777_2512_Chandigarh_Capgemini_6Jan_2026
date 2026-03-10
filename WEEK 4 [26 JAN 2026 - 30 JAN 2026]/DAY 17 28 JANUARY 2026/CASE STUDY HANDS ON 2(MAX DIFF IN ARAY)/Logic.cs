using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int diffIntArray(int[] input1)
    {
        int n = input1.Length;

        // Business Rule 2
        if (n == 1 || n > 10)
            return -2;

        // Check negatives and duplicates
        HashSet<int> set = new HashSet<int>();
        foreach (int x in input1)
        {
            if (x < 0)
                return -1;

            if (set.Contains(x))
                return -3;

            set.Add(x);
        }

        // Find max difference where larger comes after smaller
        int minSoFar = input1[0];
        int maxDiff = int.MinValue;

        for (int i = 1; i < n; i++)
        {
            if (input1[i] - minSoFar > maxDiff)
                maxDiff = input1[i] - minSoFar;

            if (input1[i] < minSoFar)
                minSoFar = input1[i];
        }

        return maxDiff;
    }
}
