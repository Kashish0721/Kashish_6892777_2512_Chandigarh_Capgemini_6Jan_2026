using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter array size: ");
        int size = int.Parse(Console.ReadLine());

        int[] arr = new int[size];
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < size; i++)
            arr[i] = int.Parse(Console.ReadLine());

        Console.Write("Enter search element: ");
        int search = int.Parse(Console.ReadLine());

        SearchRemove obj = new SearchRemove();
        int[] result = obj.RemoveAndSort(arr, size, search);

        Console.Write("Output: ");
        foreach (int x in result)
            Console.Write(x + " ");
    }
}
