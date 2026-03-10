using System;

class Logic
{
    public int CalculateSavings(int input1, int input2)
    {
       
        if (input1 > 9000)
            return -1;

        if (input1 < 0)
            return -2;

        if (input2 < 0)
            return -4;

       
        int expenses = (int)(input1 * 0.7);

        int savings = input1 - expenses;

        
        if (input2 == 31)
            savings += 500;

        return savings;
    }
}
