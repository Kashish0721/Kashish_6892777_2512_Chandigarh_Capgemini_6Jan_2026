

using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Input1 (Factor): ");
        int input1 = int.Parse(Console.ReadLine());

        Console.Write("Enter Input2 (Limit N): ");
        int input2 = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int output = obj.SumOfFactors(input1, input2);

        Console.WriteLine("Output: " + output);
    }
}
