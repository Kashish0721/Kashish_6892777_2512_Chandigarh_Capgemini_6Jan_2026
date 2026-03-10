using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of arrays: ");
        int size = int.Parse(Console.ReadLine());

        int[] input1 = new int[size];
        int[] input2 = new int[size];

        Console.WriteLine("Enter elements of Array1:");
        for (int i = 0; i < size; i++)
            input1[i] = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter elements of Array2:");
        for (int i = 0; i < size; i++)
            input2[i] = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int[] output = obj.ShipStory(input1, input2, size);

        Console.Write("Output: ");
        foreach (int x in output)
            Console.Write(x + " ");
    }
}
