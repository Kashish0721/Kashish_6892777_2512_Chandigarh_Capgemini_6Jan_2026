using System;

class SumofCubes
{
    static void Main()
    {
        Console.Write("Enter value of n: ");
        int n = int.Parse(Console.ReadLine());

 
        if (n < 0 || n > 7)
        {
            Console.WriteLine("Output = -1");
            Console.ReadLine();
            return;
        }

        int sum = 0;

        for (int i = 2; i <= n; i++)
        {
            int count = 0;

            for (int j = 1; j <= i; j++)
            {
                if (i % j == 0)
                    count++;
            }

            if (count == 2)   
            {
                sum += i * i * i;
            }
        }

        Console.WriteLine("Output = " + sum);
        Console.ReadLine();
    }
}
