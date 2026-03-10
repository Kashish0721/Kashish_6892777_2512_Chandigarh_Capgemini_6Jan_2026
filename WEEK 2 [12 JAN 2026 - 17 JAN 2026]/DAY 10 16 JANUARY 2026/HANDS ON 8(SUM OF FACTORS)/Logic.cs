using System;

class Logic
{
    public int SumOfFactors(int input1, int input2)
    {
    
        if (input1 < 0)
            return -1;

        if (input2 > 32627)
            return -2;

        int sum = 0;

        for (int i = input1; i <= input2; i += input1)
        {
            sum += i;
        }

        return sum;
    }
}
