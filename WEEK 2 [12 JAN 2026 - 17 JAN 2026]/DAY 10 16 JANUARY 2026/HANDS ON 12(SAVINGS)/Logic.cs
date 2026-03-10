using System;

class Logic
{
    public int CalculateSavings(int input1, int input2)
    {
        
        if (input1 > 9000)
            return -1;

        if (input1 < 0)
            return -3;

        if (input2 < 0)
            return -4;

      
        int food = (int)(input1 * 0.50);
        int travel = (int)(input1 * 0.20);

        int savings = input1 - (food + travel);

        
        if (input2 == 31)
            savings += 500;

        return savings;
    }
}
