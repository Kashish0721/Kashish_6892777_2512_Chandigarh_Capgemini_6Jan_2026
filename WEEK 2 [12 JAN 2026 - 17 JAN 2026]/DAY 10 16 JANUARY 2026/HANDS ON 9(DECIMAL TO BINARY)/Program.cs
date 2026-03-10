using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Decimal Number: ");
        int input1 = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int[] output = obj.DecimalToBinary(input1);

        Console.Write("Output: ");
        foreach (int x in output)
            Console.Write(x + " ");
    }
}
