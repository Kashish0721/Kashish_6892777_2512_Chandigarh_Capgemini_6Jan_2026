using System;

class Logic
{
    public int CalculatePassword(int[] input1, int size)
    {
       
        if (size < 0)
            return -2;

        int sumEven = 0;
        int sumOdd = 0;

        foreach (int x in input1)
        {
           
            if (x < 0)
                return -1;

            if (x % 2 == 0)
                sumEven += x;
            else
                sumOdd += x;
        }

        int password = (sumEven + sumOdd) / 2;
        return password;
    }
}
