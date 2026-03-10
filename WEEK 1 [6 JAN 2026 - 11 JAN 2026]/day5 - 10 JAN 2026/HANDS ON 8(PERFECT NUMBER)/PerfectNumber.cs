using System;

class PerfectNumber
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int n = int.Parse(Console.ReadLine());

        if (n < 0)
        {
            Console.WriteLine("Output = -2");
            Console.ReadLine();
            return;
        }

        int sum = 0;
        for (int i = 1; i < n; i++)
        {
            if (n % i == 0)
                sum += i;
        }

        if (sum == n)
            Console.WriteLine("Output = 1");   
        else
            Console.WriteLine("Output = -1");  

        Console.ReadLine();
    }
}
