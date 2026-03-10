using System;

class Average
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6 };
        int evenSum = 0, oddSum = 0, output1 = 0;

        foreach (int x in arr)
        {
            if (x < 0)
            {
                output1 = -1;
                Console.WriteLine(output1);
                return;
            }

            if (x % 2 == 0)
                evenSum += x;
            else
                oddSum += x;
        }

        output1 = (evenSum + oddSum) / 2;
        Console.WriteLine("Output: " + output1);
    }
}
