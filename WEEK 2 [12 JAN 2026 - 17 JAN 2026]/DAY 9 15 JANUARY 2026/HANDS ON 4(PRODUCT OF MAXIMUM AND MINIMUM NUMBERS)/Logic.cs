using System;

class Logic
{
    public int ProductOfMaxMin(int[] input1, int size)
    {
       
        if (size < 0)
            return -2;

       
        foreach (int x in input1)
            if (x < 0)
                return -1;

        int max = input1[0];
        int min = input1[0];

        for (int i = 1; i < size; i++)
        {
            if (input1[i] > max)
                max = input1[i];

            if (input1[i] < min)
                min = input1[i];
        }

        return max * min;
    }
}
