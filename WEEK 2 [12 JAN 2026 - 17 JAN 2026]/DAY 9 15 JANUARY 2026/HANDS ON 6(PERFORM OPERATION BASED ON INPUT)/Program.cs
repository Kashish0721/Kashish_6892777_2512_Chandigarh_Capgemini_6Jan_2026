
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Input1: ");
        int input1 = int.Parse(Console.ReadLine());

        Console.Write("Enter Input2: ");
        int input2 = int.Parse(Console.ReadLine());

        Console.Write("Enter Input3 (1-Add,2-Sub,3-Mul,4-Div): ");
        int input3 = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int output = obj.PerformOperation(input1, input2, input3);

        Console.WriteLine("Output: " + output);
    }
}
