using System;
using System.Collections.Generic;

class SearchRemove
{
    public int[] RemoveAndSort(int[] input1, int input2, int input3)
    {
        if (input2 < 0)
            return new int[] { -2 };

        foreach (int x in input1)
            if (x < 0)
                return new int[] { -1 };

        List<int> list = new List<int>(input1);

        if (!list.Contains(input3))
            return new int[] { -3 };

        list.Remove(input3);
        list.Sort();
        return list.ToArray();
    }
}
