using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of arrays: ");
        int input3 = int.Parse(Console.ReadLine());

        int[] input1 = new int[input3];
        int[] input2 = new int[input3];

        Console.WriteLine("Enter elements of Input1:");
        for (int i = 0; i < input3; i++)
            input1[i] = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter elements of Input2:");
        for (int i = 0; i < input3; i++)
            input2[i] = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int[] output = obj.AddReverse(input1, input2, input3);

        Console.Write("Output: ");
        foreach (int x in output)
            Console.Write(x + " ");
    }
}
