using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter elements: ");
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        int count = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] > arr[i] && arr[j] % arr[i] == 0)
                {
                    count++;
                    break;
                }
            }
        }

        Console.WriteLine("Count: " + count);
    }
}
