using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] nums = { 1, 2, 2, 3, 1, 4 };
        Console.WriteLine(MaxFrequencyCount(nums));
    }

    static int MaxFrequencyCount(int[] nums)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();

        foreach (int x in nums)
            map[x] = map.ContainsKey(x) ? map[x] + 1 : 1;

        int max = 0;
        foreach (int v in map.Values)
            if (v > max) max = v;

        int count = 0;
        foreach (int v in map.Values)
            if (v == max) count += v;

        return count;
    }
}
