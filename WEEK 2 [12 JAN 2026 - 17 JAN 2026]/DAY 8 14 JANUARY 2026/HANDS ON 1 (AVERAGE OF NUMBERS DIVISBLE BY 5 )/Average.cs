using System;

class Average
{
    public int AvgDivBy5(int input1)
    {
        if (input1 < 0)
            return -1;

        int sum = 0, count = 0;

        for (int i = 1; i <= input1; i++)
        {
            if (i % 5 == 0)
            {
                sum += i;
                count++;
            }
        }

        if (count == 0)
            return 0;

        return sum / count;
    }
}
