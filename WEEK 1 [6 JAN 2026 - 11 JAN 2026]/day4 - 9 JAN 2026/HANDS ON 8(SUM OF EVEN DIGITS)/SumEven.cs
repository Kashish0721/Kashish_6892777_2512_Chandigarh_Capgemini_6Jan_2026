using System;

class SumEven
{
    static void Main()
    {
        int num, sum = 0, output;

        Console.Write("Enter number: ");
        num = int.Parse(Console.ReadLine());

        if (num < 0)
            output = -1;
        else if (num > 32767)
            output = -2;
        else
        {
            while (num > 0)
            {
                int d = num % 10;
                if (d % 2 == 0)
                    sum += d;
                num /= 10;
            }
            output = sum;
        }

        Console.WriteLine("Output: " + output);
    }
}