using System;

class ProductofDigit
{
    static void Main()
    {
        int input1 = 56;   
        int output;

        if (input1 < 0)
        {
            output = -1;
        }
        else if (input1 % 3 == 0 || input1 % 5 == 0)
        {
            output = -2;
        }
        else
        {
            int n = input1;
            int product = 1;

            while (n > 0)
            {
                product = product * (n % 10);
                n = n / 10;
            }

            if (product % 3 == 0 || product % 5 == 0)
                output = 1;
            else
                output = 0;
        }

        Console.WriteLine("Output = " + output);
        Console.ReadLine();
    }
}
