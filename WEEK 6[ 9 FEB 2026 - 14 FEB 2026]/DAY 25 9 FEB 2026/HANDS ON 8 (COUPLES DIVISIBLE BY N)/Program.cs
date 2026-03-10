using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter array size (N): ");
        int N = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter elements: ");
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        int count = 0;

        for (int i = 0; i < N - 1; i++)
        {
            int sum = arr[i] + arr[i + 1];

            if (sum % N == 0)
                count++;
        }

        Console.WriteLine("Total couples: " + count);
    }
}
