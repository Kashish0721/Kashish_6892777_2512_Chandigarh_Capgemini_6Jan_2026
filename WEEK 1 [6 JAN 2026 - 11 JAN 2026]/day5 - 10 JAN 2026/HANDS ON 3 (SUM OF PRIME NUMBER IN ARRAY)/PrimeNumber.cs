using System;
class PrimeNumber
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        int size = 5;
        int sum = 0;

        if (size < 0)
            Console.WriteLine("Output = -2");
        else
        {
            for (int i = 0; i < size; i++)
            {
                if (arr[i] < 0)
                {
                    Console.WriteLine("Output = -1");
                    return;
                }

                int n = arr[i];
                if (n > 1)
                {
                    int count = 0;
                    for (int j = 1; j <= n; j++)
                    {
                        if (n % j == 0)
                            count++;
                    }

                    if (count == 2)   
                    {
                        sum += n;
                    }
                }
            }

            if (sum == 0)
                Console.WriteLine("Output = -3");
            else
                Console.WriteLine("Output = " + sum);
        }

        Console.ReadLine();
    }
}
