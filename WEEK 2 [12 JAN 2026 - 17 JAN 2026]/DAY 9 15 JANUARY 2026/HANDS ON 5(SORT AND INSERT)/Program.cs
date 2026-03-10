using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());

        int[] input1 = new int[size];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < size; i++)
            input1[i] = int.Parse(Console.ReadLine());

        Console.Write("Enter element to insert: ");
        int insert = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int[] output = obj.SortAndInsert(input1, size, insert);

        Console.Write("Output: ");
        foreach (int x in output)
            Console.Write(x + " ");
    }
}
