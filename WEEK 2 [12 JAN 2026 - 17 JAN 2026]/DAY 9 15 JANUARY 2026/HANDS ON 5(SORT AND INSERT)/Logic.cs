using System;
using System.Collections.Generic;

class Logic
{
    public int[] SortAndInsert(int[] input1, int size, int insert)
    {
        // B.R2
        if (size < 0)
            return new int[] { -2 };

        // B.R1
        foreach (int x in input1)
            if (x < 0)
                return new int[] { -1 };

        List<int> list = new List<int>(input1);
        list.Add(insert);
        list.Sort();

        return list.ToArray();
    }
}
