using System;

class Program
{
    static int CountCouples(int[] arr, int N)
    {
        int count = 0;

        for (int i = 0; i < arr.Length - 1; i++)
        {
            if ((arr[i] + arr[i + 1]) % N == 0)
                count++;
        }

        return count;
    }

    static void Main()
    {
        Console.Write("Enter array size: ");
        int N = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[N];

        Console.WriteLine("Enter elements:");
        for (int i = 0; i < N; i++)
            arr[i] = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Total couples: " + CountCouples(arr, N));
    }
}
