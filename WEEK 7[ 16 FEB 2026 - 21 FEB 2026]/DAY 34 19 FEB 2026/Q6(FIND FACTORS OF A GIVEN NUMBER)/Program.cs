using System;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        if (n == 0)
        {
            Console.WriteLine("No Factors");
            return;
        }

        n = Math.Abs(n);

        for (int i = 1; i <= n; i++)
        {
            if (n % i == 0)
                Console.Write(i + (i < n ? ", " : ""));
        }
    }
}
 