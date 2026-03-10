using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a Positive number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        int sum = 0;

        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }
        Console.WriteLine("sum of digits: " + sum);
    }
}