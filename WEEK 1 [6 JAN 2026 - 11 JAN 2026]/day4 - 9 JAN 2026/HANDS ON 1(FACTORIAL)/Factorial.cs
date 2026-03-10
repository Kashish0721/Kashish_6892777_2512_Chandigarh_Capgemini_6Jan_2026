using System;

class Factorial
{
    static void Main()
    {
        int num;
        int output1 = 0;

        Console.Write("Enter a number: ");
        num = int.Parse(Console.ReadLine());

        if (num < 0)
        {
            output1 = -1;
        }
        else if (num > 7)
        {
            output1 = -2;
        }
        else
        {
            output1 = 1;
            for (int i = 1; i <= num; i++)
            {
                output1 = output1 * i;
            }
        }

        Console.WriteLine("Output: " + output1);
    }
}
