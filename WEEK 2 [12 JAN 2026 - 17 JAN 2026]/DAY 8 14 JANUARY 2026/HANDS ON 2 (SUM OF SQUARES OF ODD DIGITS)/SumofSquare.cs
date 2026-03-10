using System;

class SumofSquare
{
    public int SumOfSquaresOddDigits(int input1)
    {
        if (input1 < 0)
            return -1;

        int sum = 0;
        while (input1 > 0)
        {
            int d = input1 % 10;
            if (d % 2 != 0)
                sum += d * d;
            input1 /= 10;
        }
        return sum;
    }
}
