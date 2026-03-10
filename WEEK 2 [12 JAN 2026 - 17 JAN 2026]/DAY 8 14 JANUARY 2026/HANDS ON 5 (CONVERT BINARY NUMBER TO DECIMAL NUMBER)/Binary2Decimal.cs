using System;

class Binary2Decimal
{
    public int BinaryToDecimal(string input1)
    {
        if (input1.Length > 5)
            return -2;

        foreach (char c in input1)
            if (c != '0' && c != '1')
                return -1;

        int dec = 0;
        foreach (char c in input1)
            dec = dec * 2 + (c - '0');

        return dec;
    }
}
