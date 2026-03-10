using System;

class CountDigits
{
    static void Main()
    {
        int num, count = 0, output1;

        Console.Write("Enter number: ");
        num = int.Parse(Console.ReadLine());

        if (num < 0)
            output1 = -1;
        else
        {
            if (num == 0) count = 1;
            while (num > 0)
            {
                count++;
                num /= 10;
            }
            output1 = count;
        }

        Console.WriteLine("Output: " + output1);
    }
}
