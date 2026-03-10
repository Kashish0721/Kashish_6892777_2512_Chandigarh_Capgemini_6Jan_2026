using System;

class Program
{
    static int SumOfDigits(long num)
    {
        int sum = 0;

        while (num > 0)
        {
            sum += (int)(num % 10);
            num /= 10;
        }

        return sum;
    }

    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        long num = Convert.ToInt64(Console.ReadLine());

        Console.WriteLine("Sum of digits: " + SumOfDigits(num));
    }
}
