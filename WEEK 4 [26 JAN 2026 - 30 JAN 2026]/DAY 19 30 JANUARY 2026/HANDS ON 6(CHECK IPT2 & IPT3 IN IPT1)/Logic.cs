using System;

class Logic
{
    public static int CheckOrder(string input1, string input2, string input3)
    {
        int i2 = input1.IndexOf(input2);
        int i3 = input1.IndexOf(input3);

        if (i2 == -1 || i3 == -1)
            return -1;

        return i3 > i2 ? 1 : 0;
    }
}
