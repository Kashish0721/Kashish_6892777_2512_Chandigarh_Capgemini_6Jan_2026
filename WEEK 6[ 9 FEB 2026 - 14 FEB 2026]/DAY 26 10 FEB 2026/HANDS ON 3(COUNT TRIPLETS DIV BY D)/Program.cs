using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter array: ");
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        Console.Write("Enter divisor d: ");
        int d = int.Parse(Console.ReadLine());

        int count = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                for (int k = j + 1; k < arr.Length; k++)
                {
                    if ((arr[i] + arr[j] + arr[k]) % d == 0)
                        count++;
                }
            }
        }

        Console.WriteLine("Triplets count: " + count);
    }
}
