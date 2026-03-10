using System;

class Logic
{
    public int PerformOperation(int input1, int input2, int input3)
    {
       
        if (input1 < 0 && input2 < 0)
            return -1;

        switch (input3)
        {
            case 1: return input1 + input2;
            case 2: return input1 - input2;
            case 3: return input1 * input2;
            case 4: return input2 != 0 ? input1 / input2 : 0;
            default: return 0;
        }
    }
}
