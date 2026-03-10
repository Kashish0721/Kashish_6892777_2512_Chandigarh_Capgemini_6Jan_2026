using System;
class SumofCubes
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        if (n < 0 || n > 7)
        {
            Console.WriteLine("-1");
            Console.ReadLine();
            return;
        }

        int sum = 0;
        for (int i = 2; i <= n; i++)
        {
            int c = 0;
            for (int j = 1; j <= i; j++)
                if (i % j == 0) c++;

            if (c == 2) sum += i * i * i;
        }

        Console.WriteLine(sum);
        Console.ReadLine();
    }
}
