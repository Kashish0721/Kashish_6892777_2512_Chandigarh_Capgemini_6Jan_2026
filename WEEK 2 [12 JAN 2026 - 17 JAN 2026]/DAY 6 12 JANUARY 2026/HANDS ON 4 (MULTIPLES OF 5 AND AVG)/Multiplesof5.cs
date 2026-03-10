using System;

class Multiplesof5
{
    static void Main()
    {
        Console.Write("Enter array size: ");
        int size = int.Parse(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("Output = -2");
            Console.ReadLine();
            return;
        }

        int[] arr = new int[size];
        int sum = 0;
        int count = 0;

        Console.WriteLine("Enter array elements:");

        for (int i = 0; i < size; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());

            if (arr[i] < 0)
            {
                Console.WriteLine("Output = -1");
                Console.ReadLine();
                return;
            }

            if (arr[i] % 5 == 0)
            {
                sum += arr[i];
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Output = 0");
        }
        else
        {
            int avg = sum / count;
            Console.WriteLine("Sum = " + sum);
            Console.WriteLine("Average = " + avg);
        }

        Console.ReadLine();
    }
}
