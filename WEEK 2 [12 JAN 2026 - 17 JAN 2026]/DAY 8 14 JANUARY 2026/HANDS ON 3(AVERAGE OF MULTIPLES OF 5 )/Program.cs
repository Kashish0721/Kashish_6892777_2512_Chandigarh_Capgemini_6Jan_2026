using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter N: ");
        int input1 = int.Parse(Console.ReadLine());

        Multiple obj = new Multiple();
        int output = obj.AvgMultiplesOfFive(input1);

        Console.WriteLine("Output: " + output);
    }
}
