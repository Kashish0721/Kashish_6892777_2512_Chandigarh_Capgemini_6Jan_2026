using System;

class Armstrong
{
    static void Main()
    {
        int num, output1 = 0, sum = 0, temp, digit;

        Console.Write("Enter number: ");
        num = int.Parse(Console.ReadLine());

        if (num < 0)
            output1 = -1;
        else if (num > 999)
            output1 = -2;
        else
        {
            temp = num;
            while (temp > 0)
            {
                digit = temp % 10;
                sum += digit * digit * digit;
                temp /= 10;
            }

            output1 = (sum == num) ? 1 : 0;
        }

        Console.WriteLine("Output: " + output1);
    }
}
