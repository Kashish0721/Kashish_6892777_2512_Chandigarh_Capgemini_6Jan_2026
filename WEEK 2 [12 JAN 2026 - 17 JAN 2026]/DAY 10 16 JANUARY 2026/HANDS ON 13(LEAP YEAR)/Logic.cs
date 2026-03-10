using System;

class Logic
{
    public int CheckLeapYear(int input1)
    {
        
        if (input1 < 0)
            return -1;

        if ((input1 % 400 == 0) || (input1 % 4 == 0 && input1 % 100 != 0))
            return 1;  
        else
            return 0;   
    }
}
