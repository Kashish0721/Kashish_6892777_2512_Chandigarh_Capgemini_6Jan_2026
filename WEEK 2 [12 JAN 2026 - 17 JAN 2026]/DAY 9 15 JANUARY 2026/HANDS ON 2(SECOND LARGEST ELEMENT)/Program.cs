
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int input3 = int.Parse(Console.ReadLine());

        int[] input1 = new int[input3];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < input3; i++)
            input1[i] = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int output = obj.SecondLargest(input1, input3);

        Console.WriteLine("Output: " + output);
    }
}
