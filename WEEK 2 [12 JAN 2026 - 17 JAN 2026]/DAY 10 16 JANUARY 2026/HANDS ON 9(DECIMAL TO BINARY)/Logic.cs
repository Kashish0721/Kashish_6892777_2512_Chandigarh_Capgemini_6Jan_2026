using System;
using System.Collections.Generic;

class Logic
{
    public int[] DecimalToBinary(int input1)
    {
        if (input1 < 0)
            return new int[] { -1 };

        if (input1 == 0)
            return new int[] { 0 };

        List<int> bin = new List<int>();

        while (input1 > 0)
        {
            bin.Add(input1 % 2);
            input1 /= 2;
        }

        bin.Reverse();
        return bin.ToArray();
    }
}
